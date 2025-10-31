using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.DTO;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Services
{
    /// <summary>
    /// Сервис для работы с ордерами (создание, получение, обновление, удаление).
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Получает список ордеров по фильтру (фильтр по предмету/языку/платформе).
        /// </summary>
        Task<OrderWithUser[]> GetOrdersAsync(OrderFilter orderFilter);

        /// <summary>
        /// Получает ордера пользователя по его userId.
        /// </summary>
        Task<Order[]> GetUserOrdersByUserIdAsync(string userId);

        /// <summary>
        /// Получает ордера пользователя по его slug.
        /// </summary>
        Task<Order[]> GetUserOrdersBySlugAsync(string slug);

        /// <summary>
        /// Получает ордера текущего аутентифицированного пользователя. 🔒
        /// </summary>
        Task<Order[]> GetMyOrdersAsync(string jwt);

        /// <summary>
        /// Получает подробную информацию об ордере.
        /// </summary>
        Task<OrderWithUser> GetOrderInfoAsync(string id);

        /// <summary>
        /// Создаёт новый ордер (подробная сигнатура). 🔒
        /// </summary>
        Task<Order> PostOrderAsync(
            string itemId,
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
            bool visible = true);

        /// <summary>
        /// Создаёт новый ордер (через модель запроса). 🔒
        /// </summary>
        Task<Order> PostOrderAsync(PostOrderRequest body, string jwt);

        /// <summary>
        /// Обновляет существующий ордер. 🔒
        /// </summary>
        Task<Order> UpdateOrderAsync(string id, PatchOrderRequest body, string jwt);

        /// <summary>
        /// Удаляет ордер. 🔒
        /// </summary>
        Task<bool> DeleteOrderAsync(string id, string jwt);
    }
}
