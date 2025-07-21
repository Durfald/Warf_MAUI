using Newtonsoft.Json;
using System.Diagnostics;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2
{
    public class UserShort
    {
        [JsonProperty("id")]
        public string? Id { get; set; }  // Уникальный идентификатор пользователя.

        [JsonProperty("ingameName")]
        public string? IngameName { get; set; }  // Имя пользователя в игре.

        [JsonProperty("avatar")]
        public string? Avatar { get; set; }  // Изображение аватара пользователя.

        [JsonProperty("reputation")]
        public short Reputation { get; set; }  // Репутация пользователя.

        [JsonProperty("locale")]
        public string? Locale { get; set; }  // Предпочитаемый язык общения.

        [JsonProperty("Platform")]
        public Platform? Platform { get; set; }  // Платформа, на которой играет пользователь.

        [JsonProperty("crossplay")]
        public bool Crossplay { get; set; }  // Участвует ли пользователь в кросс-платформенной игре.

        [JsonProperty("status")]
        public UserStatus? Status { get; set; }  // Текущий статус пользователя.

        [JsonProperty("activity")]
        public Activity? Activity { get; set; }  // Текущая активность пользователя.

        [JsonProperty("lastSeen")]
        public DateTime? LastSeen { get; set; }  // Время последнего появления пользователя в сети.
    }
}
