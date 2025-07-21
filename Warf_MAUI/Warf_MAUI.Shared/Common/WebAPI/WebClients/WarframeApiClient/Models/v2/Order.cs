using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2
{
    public class Order
    {
        [JsonProperty("id")]
        public string? Id { get; set; }  // Unique identifier of the order / Уникальный идентификатор ордера

        [JsonProperty("type")]
        public OrderType Type { get; set; } // Specifies whether the order is a 'buy' or 'sell' / Указывает, является ли ордер 'покупка' или 'продажа'

        [JsonProperty("platinum")]
        public int Platinum { get; set; }  // Total platinum involved in the order / Общее количество платины в ордере

        [JsonProperty("quantity")]
        public int Quantity { get; set; }  // Number of items in the order / Количество предметов в ордере

        [JsonProperty("perTrade")]
        public int? PerTrade { get; set; }  // Quantity per transaction (optional) / Количество на одну сделку (опционально)

        [JsonProperty("rank")]
        public int? Rank { get; set; }  // Rank or level of the item (optional) / Ранг или уровень предмета (опционально)

        [JsonProperty("charges")]
        public int? Charges { get; set; }  // Number of charges left (optional) / Количество оставшихся зарядов (опционально)

        [JsonProperty("subtype")]
        public string? Subtype { get; set; }  // Specific subtype of the item (optional) / Специфический подтип предмета (опционально)

        [JsonProperty("amberStars")]
        public int? AmberStars { get; set; }  // Count of amber stars (optional) / Количество янтарных звезд (опционально)

        [JsonProperty("cyanStars")]
        public int? CyanStars { get; set; }  // Count of cyan stars (optional) / Количество циановых звезд (опционально)

        [JsonProperty("visible")]
        public bool Visible { get; set; }  // Indicates if the order is publicly visible / Указывает, видим ли ордер публично

        [JsonProperty("createdAt")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }  // Creation time of the order / Время создания ордера

        [JsonProperty("updatedAt")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }  // Last modification time of the order / Время последнего изменения ордера

        [JsonProperty("itemId")]
        public string? ItemId { get; set; }  // Unique identifier of the item / Уникальный идентификатор предмета

        [JsonProperty("group")]
        public string? Group { get; set; }  // User-defined group of the order / Пользовательская группа ордера

        public override string ToString()
        {
            return $"Order: Id={Id}, Type={Type}, Platinum={Platinum}, Qty={Quantity}, Visible={Visible}";
        }
    }
}
