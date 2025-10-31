using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v1
{
    internal class RootPayload<T> where T : new()
    {
        [JsonProperty("payload")]
        public T Payload { get; set; } = new T();
    }
}
