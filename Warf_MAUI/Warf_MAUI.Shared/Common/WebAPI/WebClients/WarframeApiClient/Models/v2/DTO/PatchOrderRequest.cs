using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.DTO
{
    public class PatchOrderRequest
    {
        [JsonProperty("platinum", NullValueHandling = NullValueHandling.Ignore)]
        public int? Platinum { get; set; }

        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantity { get; set; }

        [JsonProperty("perTrade", NullValueHandling = NullValueHandling.Ignore)]
        public int? PerTrade { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public int? Rank { get; set; }

        [JsonProperty("charges", NullValueHandling = NullValueHandling.Ignore)]
        public int? Charges { get; set; }

        [JsonProperty("amberStars", NullValueHandling = NullValueHandling.Ignore)]
        public int? AmberStars { get; set; }

        [JsonProperty("cyanStars", NullValueHandling = NullValueHandling.Ignore)]
        public int? CyanStars { get; set; }

        [JsonProperty("subtype", NullValueHandling = NullValueHandling.Ignore)]
        public string? Subtype { get; set; }

        [JsonProperty("visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }
    }
}
