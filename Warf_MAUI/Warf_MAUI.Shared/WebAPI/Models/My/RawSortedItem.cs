using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.WebAPI.Models.My
{
    internal class RawSortedItem
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("url_name")]
        public string UrlName { get; set; } = null!;

        [JsonProperty("spread")]
        public int Spread { get; set; }

        [JsonProperty("rank")]
        public double? Rank { get; set; }

        [JsonProperty("buy_price")]
        public int BuyPrice { get; set; }

        [JsonProperty("sell_price")]
        public int SellPrice { get; set; }

        [JsonProperty("trading_tax")]
        public int TradingTax { get; set; }

        [JsonProperty("90days_trend")]
        public double DaysTrend { get; set; }

        [JsonProperty("48hours_trend")]
        public double HoursTrend { get; set; }
    }
}