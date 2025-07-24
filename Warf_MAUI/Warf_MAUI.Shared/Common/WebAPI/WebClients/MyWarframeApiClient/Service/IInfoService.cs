using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Service
{
    public interface IInfoService
    {
        Task<StatusDto> GetStatusAsync(string language = "ru");
        Task<SortedItemsResult> GetSortedItemsAsync(string language = "ru", int minSpread = 15, int minLiquidity = 60);
        Task<IEnumerable<ItemShort>> GetDetailsAsync(string language = "ru");
        Task<List<DucatTrade>> GetBestDucatTradesAsync(string language = "ru", int count = 100);

        Task<ItemShort> GetItemDetailsAsync(string itemId, string language = "ru");
    }
}
