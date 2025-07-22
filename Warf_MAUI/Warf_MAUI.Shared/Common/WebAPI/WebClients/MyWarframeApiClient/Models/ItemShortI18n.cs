using Newtonsoft.Json;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models
{
    // Localized representation of item short data.
    // Локализованное представление краткой информации о предмете.
    public class ItemShortI18n
    {
        [JsonProperty("name")]
        public string? Name { get; set; }  // Localized name / Локализованное название.

        [JsonProperty("icon")]
        public string? Icon { get; set; }  // URL to the icon / Ссылка на иконку.

        [JsonProperty("thumb")]
        public string? Thumb { get; set; }  // URL to the thumbnail / Ссылка на миниатюру.

        [JsonProperty("subIcon")]
        public string? SubIcon { get; set; }  // Optional sub-icon URL / Необязательная ссылка на дополнительную иконку.
    }
}
