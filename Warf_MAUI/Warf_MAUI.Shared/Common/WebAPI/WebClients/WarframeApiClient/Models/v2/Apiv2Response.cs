using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2
{
    public class Apiv2Response<T>
    {
        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; } = null!;

        [JsonProperty("data")]
        public T? Data { get; set; }

        [JsonProperty("error")]
        public dynamic? Error { get; set; }
    }
}
