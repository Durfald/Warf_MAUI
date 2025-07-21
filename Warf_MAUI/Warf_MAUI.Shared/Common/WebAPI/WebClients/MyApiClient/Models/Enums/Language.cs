using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyApiClient.Models.Enums
{
    /// <summary>
    /// One of the languages supported by the backend. 
    ///Like, Frontend can have more languages, but they are only used to translate the user interface.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Language
    {
        [EnumMember(Value = "ko")]
        Korean,     // Korean / Корейский

        [EnumMember(Value = "ru")]
        Russian,    // Russian / Русский

        [EnumMember(Value = "de")]
        German,     // German / Немецкий

        [EnumMember(Value = "fr")]
        French,     // French / Французский

        [EnumMember(Value = "pt")]
        Portuguese, // Portuguese / Португальский

        [EnumMember(Value = "zh-hans")]
        ChineseSimplified, // Simplified Chinese / Китайский упрощённый

        [EnumMember(Value = "zh-hant")]
        ChineseTraditional, // Traditional Chinese / Китайский традиционный

        [EnumMember(Value = "es")]
        Spanish,    // Spanish / Испанский

        [EnumMember(Value = "it")]
        Italian,    // Italian / Итальянский

        [EnumMember(Value = "pl")]
        Polish,     // Polish / Польский

        [EnumMember(Value = "uk")]
        Ukrainian,  // Ukrainian / Украинский

        [EnumMember(Value = "en")]
        English,     // English / Английский
    }
}
