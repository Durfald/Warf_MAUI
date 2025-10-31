using System;
using System.Threading.Tasks;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v1;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.DTO;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Services;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient
{
    /// <summary>
    /// Клиент для взаимодействия с Warframe.market (упрощённо).
    /// Событие LoggedIn вызывается после успешного получения JWT пользователя.
    /// </summary>
    public class WarframeMarketApiClient
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        private string _jwt = string.Empty;
        public string JWT { get => _jwt; private set => _jwt = value; }

        public WarframeMarketApiClient(IOrderService orderService, IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService;
        }

        /// <summary>
        /// Вызывается после успешного логина.
        /// </summary>
        public event EventHandler<UserEventArgs>? LoggedIn;

        protected virtual void OnLoggedIn(User user)
        {
            LoggedIn?.Invoke(this, new UserEventArgs(user));
        }

        /// <summary>
        /// Выполняет логин: получает CSRF (при необходимости), затем JWT. По успешному логину — генерирует событие.
        /// </summary>
        public async Task<User> LogIn(string email, string password)
        {
            // Получаем CSRF токен (если сервис требует предварительного запроса).
            // Предыдущая версия вызывала GetCSRFTokenAsync без await — теперь корректно ожидаем результат.
            //var csrfToken = await _authService.GetCSRFTokenAsync();

            var user = await _authService.GetJwtTokenAsync(email, password);
            JWT = user.JwtToken;

            // Генерируем событие о входе
            OnLoggedIn(user);

            return user;
        }

        public async Task<Order> CreateOrder(PostOrderRequest body)
        {
            var order = await _orderService.PostOrderAsync(body, JWT);
            return order;
        }

        public async Task<Order[]> GetUserOrdersBySlugAsync(string slug)
        {
            var orders = await _orderService.GetUserOrdersBySlugAsync(slug);
            return orders;
        }

        public async Task<Order[]> GetUserOrdersByUserIdAsync(string userId)
        {
            var orders = await _orderService.GetUserOrdersByUserIdAsync(userId);
            return orders;
        }

        public async Task<OrderWithUser[]> GetOrdersAsync(OrderFilter orderFilter)
        {
            var orders = await _orderService.GetOrdersAsync(orderFilter);
            return orders;
        }

        public async Task<Order> GetOrderInfoAsync(string id)
        {
            var order = await _orderService.GetOrderInfoAsync(id);
            return order;
        }

        public async Task<Order> UpdateOrderAsync(string id, PatchOrderRequest body)
        {
            var order = await _orderService.UpdateOrderAsync(id, body, JWT);
            return order;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var status = await _orderService.DeleteOrderAsync(id, JWT);
            return status;
        }

        public async Task<Order[]> GetMyOrdersAsync(string JWT)
        {
            var orders = await _orderService.GetMyOrdersAsync(JWT);
            return orders;
        }

    }

    /// <summary>
    /// Аргументы события LoggedIn.
    /// </summary>
    public class UserEventArgs : EventArgs
    {
        public User User { get; }

        public UserEventArgs(User user)
        {
            User = user;
        }
    }
}
