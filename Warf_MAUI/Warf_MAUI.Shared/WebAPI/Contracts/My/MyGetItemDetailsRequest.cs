using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.WebAPI.Contracts.My
{
    internal class MyGetItemDetailsRequest
    {
        [JsonProperty("language")]
        public string HLanguage { get; set; } = "ru";
    }
}