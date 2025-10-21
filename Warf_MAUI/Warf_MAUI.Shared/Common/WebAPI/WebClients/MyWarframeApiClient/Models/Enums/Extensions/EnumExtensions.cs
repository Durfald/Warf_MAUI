using System.Reflection;
using System.Runtime.Serialization;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models.Enums.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ParseFromEnumMemberValue<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, Enum
        {
            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
                if (attribute != null)
                {
                    if (string.Equals(attribute.Value, value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                        return (TEnum)field.GetValue(null);
                }

                if (string.Equals(field.Name, value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    return (TEnum)field.GetValue(null);
            }

            throw new ArgumentException($"Unknown value '{value}' for enum {typeof(TEnum).Name}");
        }
    }
}
