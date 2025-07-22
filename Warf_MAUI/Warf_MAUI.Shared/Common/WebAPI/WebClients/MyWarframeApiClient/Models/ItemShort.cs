using Newtonsoft.Json;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models.Enums;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models
{
    // Represents a summary of an item in the game.
    // Представляет краткую информацию об игровом предмете.
    //depends on your Language header, you could have any other language inside i18n field
    //зависит от вашего заголовка Language, вы можете иметь любой другой язык в поле i18n
    public class ItemShort
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!; // Unique identifier of the item / Уникальный идентификатор предмета.

        [JsonProperty("Slug")]
        public string Slug { get; set; } = null!; // URL-friendly name of the item / Название предмета в формате URL.

        [JsonProperty("gameRef")]
        public string? GameRef { get; set; }  // Reference to the item in the game's database / Ссылка на предмет в базе данных игры.

        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }  // Tags associated with the item / Теги, связанные с предметом.

        [JsonProperty("i18n")]
        public Dictionary<Language, ItemShortI18n> I18n { get; set; } = null!; // Localized text for the item in various languages / Локализованные данные предмета на разных языках.

        [JsonProperty("maxRank")]
        public int? MaxRank { get; set; }  // Maximum rank the item can achieve / Максимальный ранг предмета.

        [JsonProperty("maxCharges")]
        public int? MaxCharges { get; set; }  // Maximum charges (for requiem mods) / Максимальное количество зарядов (для реквием-модов).

        [JsonProperty("vaulted")]
        public bool? Vaulted { get; set; }  // Whether the item is vaulted / Является ли предмет архивированным (vaulted).

        [JsonProperty("bulkTradable")]
        public bool? BulkTradable { get; set; }  // Whether the item can be traded in bulk / Можно ли торговать предметом оптом.

        [JsonProperty("ducats")]
        public short? Ducats { get; set; }  // Ducat value of the item / Стоимость предмета в дукатах.

        [JsonProperty("maxAmberStars")]
        public int? MaxAmberStars { get; set; }  // Number of amber stars associated with the item / Количество янтарных звёзд у предмета.

        [JsonProperty("maxCyanStars")]
        public int? MaxCyanStars { get; set; }  // Number of cyan stars associated with the item / Количество голубых звёзд у предмета.

        [JsonProperty("baseEndo")]
        public short? BaseEndo { get; set; }  // Base Endo value of the item / Базовое значение эндо.

        [JsonProperty("EndoMultiplier")]
        public float? EndoMultiplier { get; set; }  // Endo multiplier for the item / Множитель эндо для предмета.

        [JsonProperty("Subtypes")]
        public object? Subtypes { get; set; }  // Subtypes of the item (if any) / Подтипы предмета (если есть).

        public override string ToString()
        {
            return $"Id={Id}, Slug={Slug}";
        }
    }
}
