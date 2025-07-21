using Newtonsoft.Json;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.DTO
{
    public class PostOrderRequest
    {
        [JsonProperty("itemId")]
        public required string ItemId { get; set; } = null!;

        [JsonProperty("type")]
        public required OrderType Type { get; set; }

        [JsonProperty("platinum")]
        public required int Platinum { get; set; }

        [JsonProperty("quantity")]
        public required int Quantity { get; set; }

        [JsonProperty("visible")]
        public required bool Visible { get; set; } = true;

        [JsonProperty("perTrade", NullValueHandling = NullValueHandling.Ignore)]
        public int PerTrade { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public int? Rank { get; set; }

        [JsonProperty("charges", NullValueHandling = NullValueHandling.Ignore)]
        public int? Charges { get; set; }

        [JsonProperty("subtype", NullValueHandling = NullValueHandling.Ignore)]
        public string? Subtype { get; set; }

        [JsonProperty("amberStars", NullValueHandling = NullValueHandling.Ignore)]
        public int? AmberStars { get; set; }

        [JsonProperty("cyanStars", NullValueHandling = NullValueHandling.Ignore)]
        public int? CyanStars { get; set; }
    }
}
