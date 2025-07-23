using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients
{
    public class CombinedApiClient
    {
        public WarframeMarketApiClient Warframe { get; }
        public MyApiClient My { get; }

        public CombinedApiClient(MyApiClient my, WarframeMarketApiClient warframe)
        {
            My = my;
            Warframe = warframe;
        }
    }
}