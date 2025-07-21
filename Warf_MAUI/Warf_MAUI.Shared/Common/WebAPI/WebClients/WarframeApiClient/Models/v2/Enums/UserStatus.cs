using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserStatus
    {
        [EnumMember(Value = "invisible")]
        Invisible,

        [EnumMember(Value = "offline")]
        Offline,

        [EnumMember(Value = "online")]
        Online,

        [EnumMember(Value = "ingame")]
        InGame
    }
}
