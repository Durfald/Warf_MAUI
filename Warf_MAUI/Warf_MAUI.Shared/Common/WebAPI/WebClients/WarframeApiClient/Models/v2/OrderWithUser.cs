using Newtonsoft.Json;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2
{
    public class OrderWithUser : Order
    {
        [JsonProperty("user")]
        public UserShort User { get; set; } = null!;  // Represents the user who created the order, with basic profile information. / Представляет пользователя, создавшего ордер, с базовой информацией о профиле.

        public override string ToString()
        {
            return $"OrderWithUser: Id={Id}, Type={Type}, Platinum={Platinum}, Quantity={Quantity}, User={User?.IngameName ?? "unknown"} (Reputation={User?.Reputation})";
        }

    }
}
