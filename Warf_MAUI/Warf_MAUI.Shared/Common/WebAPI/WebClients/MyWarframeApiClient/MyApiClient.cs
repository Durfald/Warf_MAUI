using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Service;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient
{
    public class MyApiClient
    {

        private readonly IInfoService _myInfoService;

        public MyApiClient(IInfoService myInfoService)
        {
            _myInfoService = myInfoService;
        }

        public Task<StatusDto> GetStatusAsync(string language = "ru") => _myInfoService.GetStatusAsync(language);

        public Task<SortedItemsResult> GetSortedItemsAsync(string language = "ru", int minSpread = 15, int minLiquidity = 60) => _myInfoService.GetSortedItemsAsync(language, minSpread, minLiquidity);

        public Task<BestDucatOffersResult> GetBestDucatTradesAsync(string language = "ru", int count = 100) => 
            _myInfoService.GetBestDucatTradesAsync(language, count);

        public Task<BestEndoOffersResult> GetBestEndoTradesAsync(string language = "ru", int count = 100) =>
            _myInfoService.GetBestEndoOffersAsync(language, count);

        public Task<IEnumerable<ItemShort>> GetDetailsAsync(string language = "ru") => _myInfoService.GetDetailsAsync(language);

        public Task<ItemShort> GetItemDetailsAsync(string itemId, string language = "ru")
        {
            if (string.IsNullOrWhiteSpace(itemId))
                throw new ArgumentException("Item ID cannot be null or empty.", nameof(itemId));

            return _myInfoService.GetItemDetailsAsync(itemId, language);
        }
    }
}