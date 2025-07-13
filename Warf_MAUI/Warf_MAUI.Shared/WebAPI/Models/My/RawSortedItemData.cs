using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.WebAPI.Models.My
{
    internal class RawSortedItemData
    {
        [JsonProperty("48hours")]
        public RawSortedItem[] Last48Hours { get; set; } = null!;

        [JsonProperty("90days")]
        public RawSortedItem[] Last90Days { get; set; } = null!;
    }
}
