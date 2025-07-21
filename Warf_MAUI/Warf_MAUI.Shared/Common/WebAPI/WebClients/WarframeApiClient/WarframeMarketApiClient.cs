using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v1;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.DTO;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Services;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient
{
    public class WarframeMarketApiClient
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public WarframeMarketApiClient(IOrderService orderService, IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService;
        }

        public async Task<User> LogIn(string email, string password)
        {
            var csrf_token = _authService.GetCSRFTokenAsync();

            var user = await _authService.GetJwtTokenAsync(email, password);
            return user;
        }

        public async Task<Order> CreateOrder(PostOrderRequest body)
        {
            var order = await _orderService.PostOrderAsync(body);
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
            var order = await _orderService.UpdateOrderAsync(id, body);
            return order;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var status = await _orderService.DeleteOrderAsync(id);
            return status;
        }

        public async Task<Order[]> GetMyOrdersAsync(string JWT)
        {
            var orders = await _orderService.GetMyOrdersAsync(JWT);
            return orders;
        }

    }
}
