using Microsoft.Extensions.Logging;
using Warf_MAUI.Shared.Common.WebAPI.Interfaces;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.DTO;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientV2 _httpClient_v2;

        public OrderService(IHttpClientV2 httpClient_v2)
        {
            _httpClient_v2 = httpClient_v2;
        }

        public async Task<OrderWithUser[]> GetOrdersAsync(OrderFilter orderFilter)
        {
            if (orderFilter == null)
                throw new ArgumentNullException(nameof(orderFilter));

            var platform = orderFilter.Platform?.ToString().ToLower() ?? "pc";
            var language = orderFilter.Language ?? "ru";

            var headers = new Dictionary<string, string>
            {
                { "Language", language },
                { "Platform", platform },
                { "Crossplay", "true" }
            };

            //_logger.LogInformation("Fetching orders for item: {Slug}, Language: {Language}, Platform: {Platform}",
            //    orderFilter.Slug, language, platform);

            var res = await _httpClient_v2.GetAsync<Apiv2Response<OrderWithUser[]>>($"orders/item/{orderFilter.Slug}", headers: headers);

            //_logger.LogInformation("Successfully retrieved {Count} orders for item: {Slug}", res.Data!.Length, orderFilter.Slug);
            return res.Data!.Data!;
        }

        public async Task<Order[]> GetUserOrdersByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");

            var res = await _httpClient_v2.GetAsync<Apiv2Response<Order[]>>($"orders/userId/{userId}");
            return res.Data!.Data!;
        }

        public async Task<Order[]> GetUserOrdersBySlugAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                throw new ArgumentNullException("slug");

            var res = await _httpClient_v2.GetAsync<Apiv2Response<Order[]>>($"orders/user/{slug}");
            return res.Data!.Data!;
        }

        /// <summary>
        /// 🔒
        /// </summary>
        /// <returns></returns>
        public async Task<Order[]> GetMyOrdersAsync(string jwt)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentNullException(nameof(jwt));
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {jwt}");
            var response = await _httpClient_v2.GetAsync<Apiv2Response<Order[]>>($"orders/my", headers: headers );

            return response.Data!.Data!;
        }

        public async Task<OrderWithUser> GetOrderInfoAsync(string id)
        {
            var response = await _httpClient_v2.GetAsync<Apiv2Response<OrderWithUser>>(
               $"order/{id}");

            return response.Data!.Data!;
        }

        /// <summary>
        /// Создаёт новый ордер (заказ) на покупку или продажу предмета. 🔒
        /// </summary>
        /// <param name="ItemId">ID предмета. Можно получить из <see href="https://www.notion.so/WFM-Api-v2-Documentation-5d987e4aa2f74b55a80db1a09932459d?pvs=21">документации</see>.</param>
        /// <param name="orderType">Тип ордера: <c>sell</c> (продажа) или <c>buy</c> (покупка).</param>
        /// <param name="platinum">Цена предмета в платинах.</param>
        /// <param name="quantity">Количество предметов в наличии для покупки или продажи.</param>
        /// <param name="perTrade">Минимальное количество предметов в одной сделке.</param>
        /// <param name="rank">Ранг предмета (например, ранг мода).</param>
        /// <param name="charges">Количество оставшихся зарядов (например, для паразон-модов).</param>
        /// <param name="subtype">Подтип предмета. Смотрите модель Item для возможных значений.</param>
        /// <param name="amberStars">Количество установленных янтарных звезд.</param>
        /// <param name="cyanStars">Количество установленных голубых звезд.</param>
        /// <param name="visible">Определяет, будет ли ордер видимым или скрытым (по умолчанию <c>true</c>).</param>
        /// <returns>Асинхронно возвращает созданный объект заказа <see cref="Order"/>.</returns>
        /// <exception cref="Exception">Если создание ордера не удалось.</exception>
        public async Task<Order> PostOrderAsync(
            string ItemId,
            OrderType orderType,
            int platinum,
            int quantity,
            int perTrade,
            int? rank,
            int? charges,
            string? subtype,
            int? amberStars,
            int? cyanStars,
            string jwt,
            bool visible = true)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentNullException(nameof(jwt));
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {jwt}");

            var body = new PostOrderRequest
            {
                ItemId = ItemId,
                Type = orderType,
                Platinum = platinum,
                Quantity = quantity,
                Visible = visible,
                PerTrade = perTrade,
                Rank = rank,
                Charges = charges,
                Subtype = subtype,
                AmberStars = amberStars,
                CyanStars = cyanStars
            };

            var response = await _httpClient_v2.PostAsync<Apiv2Response<Order>>($"order", body: body, headers: headers);

            return response.Data!.Data!;
        }

        /// <summary>
        /// Асинхронно отправляет POST-запрос для создания нового заказа. 🔒
        /// </summary>
        /// <param name="body">Тело запроса, содержащее детали заказа.</param>
        /// <returns>Асинхронно возвращает созданный объект заказа <see cref="Order"/>.</returns>
        /// <exception cref="Exception">Если ответ не содержит данных.</exception>
        public async Task<Order> PostOrderAsync(PostOrderRequest body, string jwt)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentNullException(nameof(jwt));
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {jwt}");

            var response = await _httpClient_v2.PostAsync<Apiv2Response<Order>>($"order", body: body, headers: headers);
            return response.Data!.Data!;
        }

        /// <summary>
        /// Асинхронно обновляет существующий ордер на торговой площадке. 🔒
        /// </summary>
        /// <param name="id">Идентификатор ордера, который требуется обновить (в данном методе не используется, но может быть необходим в URL или логировании).</param>
        /// <param name="body">Объект запроса <see cref="PatchOrderRequest"/>, содержащий параметры обновления ордера (например, цену, видимость, количество и т.д.).</param>
        /// <returns>
        /// Обновлённый объект <see cref="Order"/> после применения изменений.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="id"/> is null or empty.
        /// </exception>
        /// <exception cref="Exception">
        /// Генерируется, если API вернул пустой ответ или не удалось получить обновлённый ордер.
        /// </exception>
        public async Task<Order> UpdateOrderAsync(string id, PatchOrderRequest body, string jwt)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("slug");

            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentNullException(nameof(jwt));
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {jwt}");

            var response = await _httpClient_v2.PatchAsync<Apiv2Response<Order>>($"order/{id}", body: body, headers: headers);

            return response.Data!.Data!;
        }

        /// <summary>
        /// Асинхронно удаляет ордер с указанным идентификатором. 🔒
        /// </summary>
        /// <param name="id">Идентификатор ордера, который требуется удалить.</param>
        /// <returns>
        /// Возвращает <c>true</c>, если ордер был успешно удалён; в противном случае — <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Генерируется, если параметр <paramref name="id"/> равен <c>null</c> или пустой строке.
        /// </exception>
        public async Task<bool> DeleteOrderAsync(string id, string jwt)
        {

            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("slug");

            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentNullException(nameof(jwt));
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {jwt}");
            var status = await _httpClient_v2.DeleteAsync($"order/{id}", headers: headers);

            return status;
        }
    }
}