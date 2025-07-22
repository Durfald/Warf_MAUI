using Newtonsoft.Json;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models
{
    public class StatusDto
    {
        [JsonProperty("isDownloading")]
        public bool IsDownloading { get; set; }

        [JsonProperty("isSorting")]
        public bool IsSorting { get; set; }
    }
}
