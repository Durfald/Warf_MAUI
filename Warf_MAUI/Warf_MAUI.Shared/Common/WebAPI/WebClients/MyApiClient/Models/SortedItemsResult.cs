using Newtonsoft.Json;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyApiClient.Models
{
    public class SortedItemsResult
    {
        [JsonProperty("48hours")]
        public Item[] Last48Hours { get; set; } = null!;

        [JsonProperty("90days")]
        public Item[] Last90Days { get; set; } = null!;
    }
}
