using Warf_MAUI.Shared.Common.Search.Models;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models;

namespace Warf_MAUI.Shared.Common.Extensions
{
    public static class GetImage
    {
        public static string GetItemImageUrl(ItemShort item)
        {
            if (item == null)
                return "_content/Warf_MAUI.Shared/svg/ducat_light.svg"; // Default image if itemShort is null
            if (item.I18n.First().Value.Icon == null)
                return "_content/Warf_MAUI.Shared/svg/ducat_light.svg";
            if(item.Tags!.Contains("component") || item.Tags!.Contains("blueprint"))
            {
                return "https://warframe.market/static/assets/" + item.I18n.First().Value.SubIcon;
            }

            return "https://warframe.market/static/assets/" + item.I18n.First().Value.Icon;
        }
        public static string GetItemImageUrl(SearchItem item)
        {
            if (item == null)
                return "_content/Warf_MAUI.Shared/svg/ducat_light.svg"; // Default image if itemShort is null
            if (item.Icon == null)
                return "_content/Warf_MAUI.Shared/svg/ducat_light.svg";
            if (item.Tags!.Contains("component") || item.Tags!.Contains("blueprint"))
            {
                return "https://warframe.market/static/assets/" + item.SubIcon;
            }
            return "https://warframe.market/static/assets/" + item.Icon;
        }
    }
}
