using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Warf_MAUI.Shared.Common.Models;
using Warf_MAUI.Shared.Common.WebAPI.WebClients;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v1;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums;
using Warf_MAUI.Shared.Pages.Configuration;

namespace Warf_MAUI.Shared.Services.Warf_MAUI.Services
{
    /// <summary>
    /// Сервис, управляющий торговлей на Warframe Market.
    /// Отвечает за создание, обновление и удаление ордеров (покупка/продажа).
    /// Работает асинхронно и автоматически обновляет цены.
    /// </summary>
    public class WarframeMarketService : INotifyPropertyChanged
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private CancellationTokenSource? _cancellationBuy;
        private CancellationTokenSource? _cancellationSell;
        private Task? _buyTask;
        private Task? _sellTask;
        private readonly ApplicationSettings _applicationSettings;
        private readonly CombinedApiClient _api;

        private User? user = null;

        public event EventHandler<MarketItem>? MarketItemChanged;
        public event EventHandler<MarketItem>? LowDifferenceOnBuy;      // когда разница слишком мала при покупке
        public event EventHandler<MarketItem>? MinDifferenceReached;    // когда цена продажи достигла разницы

        /// <summary>
        /// Активные ордера на покупку.
        /// При изменении коллекции автоматически запускается обработчик.
        /// </summary>
        public ObservableCollection<MarketItem> ListedBuyItems { get; private set; } = new();

        /// <summary>
        /// Активные ордера на продажу.
        /// </summary>
        public ObservableCollection<MarketItem> ListedSellItems { get; private set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Основной конструктор сервиса.
        /// </summary>
        /// <param name="apiClient">API-клиент, объединяющий работу с Warframe API.</param>
        /// <param name="settings">Настройки приложения (аккаунт, минимальные разницы, и т.д.).</param>
        public WarframeMarketService(CombinedApiClient apiClient, ApplicationSettings settings)
        {
            _api = apiClient;
            _applicationSettings = settings;

            // Асинхронная инициализация пользователя
            _ = InitializeUserAsync();

            // Подписки на изменения коллекций
            ListedBuyItems.CollectionChanged += (s, e) => OnListedBuyItemsChanged();
            ListedSellItems.CollectionChanged += (s, e) => OnListedSellItemsChanged();
        }

        /// <summary>
        /// Авторизация пользователя по сохранённым данным из ApplicationSettings.
        /// </summary>
        private async Task InitializeUserAsync()
        {
            if (!string.IsNullOrEmpty(_applicationSettings.Network.LoginForm.Password) &&
                !string.IsNullOrEmpty(_applicationSettings.Network.LoginForm.Email))
            {
                user = await _api.Warframe.LogIn(
                    _applicationSettings.Network.LoginForm.Email,
                    _applicationSettings.Network.LoginForm.Password
                );
            }
        }

        #region Слежение за изменением коллекций
        private void OnListedBuyItemsChanged()
        {
            foreach (var item in ListedBuyItems)
                item.PropertyChanged -= OnMarketItemPropertyChanged; // чтобы не было дублей

            foreach (var item in ListedBuyItems)
                item.PropertyChanged += OnMarketItemPropertyChanged;

            // Если появились предметы для покупки — запускаем цикл обновления
            if (ListedBuyItems.Count > 0 && _buyTask == null)
            {
                _cancellationBuy = new CancellationTokenSource();
                _buyTask = Task.Run(() => DoBuyOrdersWork(_cancellationBuy.Token));
            }
            // Если предметов нет — останавливаем задачу
            else if (ListedBuyItems.Count == 0 && _cancellationBuy != null)
            {
                _cancellationBuy.Cancel();
                _buyTask = null;
            }
        }

        private void OnListedSellItemsChanged()
        {
            foreach (var item in ListedSellItems)
                item.PropertyChanged -= OnMarketItemPropertyChanged;

            foreach (var item in ListedSellItems)
                item.PropertyChanged += OnMarketItemPropertyChanged;

            if (ListedSellItems.Count > 0 && _sellTask == null)
            {
                _cancellationSell = new CancellationTokenSource();
                _sellTask = Task.Run(() => DoSellOrdersWork(_cancellationSell.Token));
            }
            else if (ListedSellItems.Count == 0 && _cancellationSell != null)
            {
                _cancellationSell.Cancel();
                _sellTask = null;
            }
        }
        #endregion

        #region 🟢 Обработка изменения данных внутри MarketItem
        private void OnMarketItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is not MarketItem item)
                return;

            // Вызов пользовательского события
            MarketItemChanged?.Invoke(this, item);

            // Можно добавить автообновление ордера при изменении цены:
            if (e.PropertyName is nameof(MarketItem.CurrentBuyPrice) or nameof(MarketItem.CurrentSellPrice))
            {
                // Например, если хотим синхронизировать с API:
                _ = UpdateOrderIfNeededAsync(item);
            }
        }

        private async Task UpdateOrderIfNeededAsync(MarketItem item)
        {
            try
            {
                await _api.Warframe.UpdateOrderAsync(item.OrderId, new() { Platinum = item.CurrentSellPrice });
            }
            catch
            {
                // логирование ошибок
            }
        }
        #endregion

        #region Основные рабочие циклы (обновление ордеров)
        /// <summary>
        /// Цикл автоматического обновления ордеров на покупку.
        /// Проверяет актуальность цен и корректирует их.
        /// </summary>
        private async Task DoBuyOrdersWork(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await _semaphore.WaitAsync(token);
                var items = new List<MarketItem>(ListedBuyItems);

                foreach (var item in items)
                {
                    if (token.IsCancellationRequested) break;

                    try
                    {
                        var lastSellOrder = await GetLastOrder(item.UrlName, item.ModRank, item.Subtype, OrderType.Sell);
                        var lastBuyOrder = await GetLastOrder(item.UrlName, item.ModRank, item.Subtype);

                        // Если минимальная разница не соблюдается — убираем предмет
                        if (_applicationSettings.General.MinPriceDifference > lastSellOrder!.First().Platinum - item.CurrentBuyPrice)
                        {
                            DeleteItem(item);
                            continue;
                        }

                        // Проверяем, наш ли ордер первый в списке
                        if (lastBuyOrder!.First().User.IngameName == user?.InGameName)
                        {
                            // Если кто-то выставил ниже — обновляем цену
                            if (lastBuyOrder!.Last().Platinum < item.CurrentBuyPrice)
                            {
                                item.CurrentBuyPrice = lastBuyOrder!.Last().Platinum;
                                await _api.Warframe.UpdateOrderAsync(item.Id, new() { Platinum = item.CurrentBuyPrice });
                            }
                            continue;
                        }

                        // Иначе просто выставляем цену как у лучшего ордера
                        item.CurrentBuyPrice = lastBuyOrder!.First().Platinum;
                        await _api.Warframe.UpdateOrderAsync(item.Id, new() { Platinum = item.CurrentBuyPrice });
                    }
                    catch
                    {
                        // Ошибки здесь лучше логировать в будущем
                    }

                    await Task.Delay(500, token);
                }

                _semaphore.Release();
                await Task.Delay(5000, token); // Обновление каждые 5 секунд
            }
        }

        /// <summary>
        /// Цикл автоматического обновления ордеров на продажу.
        /// </summary>
        private async Task DoSellOrdersWork(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await _semaphore.WaitAsync(token);
                var items = new List<MarketItem>(ListedSellItems);

                foreach (var item in items)
                {
                    if (token.IsCancellationRequested) break;

                    try
                    {
                        var lastSellOrder = await GetLastOrder(item.UrlName, item.ModRank, item.Subtype, OrderType.Sell);

                        // Если разница меньше минимальной — повышаем цену на MinPriceDifference
                        var targetPrice = (_applicationSettings.General.MinPriceDifference >
                                           item.CurrentBuyPrice - item.CurrentSellPrice)
                            ? item.CurrentBuyPrice + _applicationSettings.General.MinPriceDifference
                            : lastSellOrder!.First().Platinum;

                        if (item.CurrentSellPrice == targetPrice &&
                       item.CurrentBuyPrice - item.CurrentSellPrice <= _applicationSettings.General.MinPriceDifference)
                        {
                            MinDifferenceReached?.Invoke(this, item);
                        }

                        // Если цена изменилась — обновляем ордер
                        if (item.CurrentSellPrice != targetPrice)
                        {
                            item.CurrentSellPrice = targetPrice;
                            await _api.Warframe.UpdateOrderAsync(item.Id, new() { Platinum = item.CurrentSellPrice });
                        }
                    }
                    catch
                    {
                        // Ошибки игнорируются, чтобы цикл не падал
                    }

                    await Task.Delay(500, token);
                }

                _semaphore.Release();
                await Task.Delay(5000, token);
            }
        }
        #endregion

        #region Работа с API
        /// <summary>
        /// Получает последние ордера по предмету, фильтруя по типу (buy/sell), мод-рангу и сабтайпу.
        /// </summary>
        public async Task<OrderWithUser[]?> GetLastOrder(
            string urlItem,
            int? modrank = null,
            string? subtype = null,
            OrderType orderType = OrderType.Buy)
        {
            var orders = await _api.Warframe.GetOrdersAsync(new() { Slug = urlItem });
            orders = orders.Where(x => x.Type == orderType).ToArray();

            var filtered = orders.Where(x => x.User.Status == UserStatus.InGame);

            if (modrank.HasValue) filtered = filtered.Where(x => x.Rank == modrank.Value);
            if (!string.IsNullOrEmpty(subtype)) filtered = filtered.Where(x => x.Subtype == subtype);

            return orderType == OrderType.Buy
                ? filtered.OrderByDescending(x => x.Platinum).ThenByDescending(x => x.UpdatedAt).Take(2).ToArray()
                : filtered.OrderBy(x => x.Platinum).ThenByDescending(x => x.UpdatedAt).Take(2).ToArray();
        }
        #endregion

        #region Управление ордерами
        /// <summary>
        /// Создает ордер на покупку и добавляет его в список.
        /// </summary>
        public async Task CreateBuyOrder(MarketItem item)
        {
            var lastBuyOrder = await GetLastOrder(item.UrlName, item.ModRank, item.Subtype);
            var lastSellOrder = await GetLastOrder(item.UrlName, item.ModRank, item.Subtype, OrderType.Sell);

            // Если разница слишком мала — пропускаем
            if (_applicationSettings.General.MinPriceDifference > lastSellOrder!.First().Platinum - lastBuyOrder!.First().Platinum)
            {
                LowDifferenceOnBuy?.Invoke(this, item);  // <--- вызов события
                DeleteItem(item);
                return;
            }

            item.CurrentBuyPrice = lastBuyOrder!.First().Platinum;
            ListedBuyItems.Add(item);

            var order = await _api.Warframe.CreateOrder(new()
            {
                ItemId = item.Id,
                Platinum = item.CurrentBuyPrice,
                Rank = item.ModRank,
                Subtype = item.Subtype,
                Quantity = 1,
                Type = OrderType.Buy
            });
            item.OrderId = order.Id!;
        }

        /// <summary>
        /// Создает ордер на продажу и добавляет его в список.
        /// </summary>
        public async Task CreateSellOrder(MarketItem item)
        {
            var lastSellOrder = await GetLastOrder(item.UrlName, item.ModRank, item.Subtype, OrderType.Sell);

            if (_applicationSettings.General.MinPriceDifference > lastSellOrder!.First().Platinum - item.CurrentBuyPrice)
            {
                item.CurrentSellPrice = item.CurrentBuyPrice + _applicationSettings.General.MinPriceDifference;
            }
            else
            {
                item.CurrentSellPrice = lastSellOrder!.First().Platinum;
            }
            ListedSellItems.Add(item);

            var order = await _api.Warframe.CreateOrder(new()
            {
                ItemId = item.Id,
                Platinum = item.CurrentSellPrice,
                Rank = item.ModRank,
                Subtype = item.Subtype,
                Quantity = 1,
                Type = OrderType.Sell
            });
            item.OrderId = order.Id!;
        }

        /// <summary>
        /// Удаляет ордер из API и из коллекции.
        /// </summary>
        public async void DeleteItem(MarketItem item)
        {
            ListedBuyItems.Remove(item);
            ListedSellItems.Remove(item);
            await _api.Warframe.DeleteOrderAsync(item.OrderId);
        }

        public async Task<Order> CreateOrder(MarketItem item)
        {
            var order = await _api.Warframe.CreateOrder(new()
            {
                ItemId = item.Id,
                Platinum = item.CurrentSellPrice,
                Rank = item.ModRank,
                Subtype = item.Subtype,
                Quantity = 1,
                Type = OrderType.Sell
            });
            return order;
        }

        /// <summary>
        /// Переводит предмет из стадии покупки в стадию продажи.
        /// </summary>
        public async void UpForSaleItem(MarketItem item)
        {
            ListedBuyItems.Remove(item);
            await CreateSellOrder(item);
        }

        /// <summary>
        /// Удаляет предмет после успешной продажи.
        /// </summary>
        public async void SoldItem(MarketItem item)
        {
            ListedSellItems.Remove(item);
            await _api.Warframe.DeleteOrderAsync(item.OrderId);
        }
        #endregion

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
