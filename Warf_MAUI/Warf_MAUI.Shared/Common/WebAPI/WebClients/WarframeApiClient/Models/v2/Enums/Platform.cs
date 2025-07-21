using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Platform
    {
        [EnumMember(Value = "pc")]
        PC,         // ПК

        [EnumMember(Value = "ps4")]
        PS4,        // PlayStation 4

        [EnumMember(Value = "xbox")]
        Xbox,       // Xbox

        [EnumMember(Value = "switch")]
        Switch,     // Nintendo Switch

        [EnumMember(Value = "mobile")]
        Mobile      // Мобильные устройства
    }
}
