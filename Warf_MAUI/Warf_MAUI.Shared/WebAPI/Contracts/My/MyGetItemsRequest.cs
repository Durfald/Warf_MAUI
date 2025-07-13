using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.WebAPI.Contracts.My
{
    internal class MyGetItemsRequest
    {
        [JsonProperty("language")]
        public string HLanguage { get; set; } = "ru";

        [JsonProperty("minSpread")]
        public int MinSpread { get; set; } = 15;

        [JsonProperty("minLiquidity")]
        public int MinLiquidity { get; set; } = 60;
    }
}
