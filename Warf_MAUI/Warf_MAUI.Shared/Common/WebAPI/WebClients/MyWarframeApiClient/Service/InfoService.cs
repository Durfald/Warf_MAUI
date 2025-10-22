using Warf_MAUI.Shared.Common.WebAPI.Interfaces;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Service
{
    public interface IInfoService
    {
        Task<StatusDto> GetStatusAsync(string language = "ru");
        Task<SortedItemsResult> GetSortedItemsAsync(string language = "ru", int minSpread = 15, int minLiquidity = 60);
        Task<IEnumerable<ItemShort>> GetDetailsAsync(string language = "ru");
        Task<BestDucatOffersResult> GetBestDucatTradesAsync(string language = "ru", int count = 100);
        Task<BestEndoOffersResult> GetBestEndoOffersAsync(string language ="ru", int count = 100);
        Task<ItemShort> GetItemDetailsAsync(string itemId, string language = "ru");
    }
    public class InfoService : IInfoService
    {
        private readonly IHttpClientMy _httpClient;

        public InfoService(IHttpClientMy httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<StatusDto> GetStatusAsync(string language = "ru")
        {
            var headers = new Dictionary<string, string> { { "language", language } };

            var statusDto = await _httpClient.GetAsync<StatusDto>("info/status", headers: headers);

            return statusDto.Data!;
        }

        public async Task<SortedItemsResult> GetSortedItemsAsync(string language = "ru", int minSpread = 15, int minLiquidity = 60)
        {
            var headers = new Dictionary<string, string> { { "language", language } };

            var queryParameters = new Dictionary<string, string> { { "minSpread", minSpread.ToString() }, { "minLiquidity", minLiquidity.ToString() } };

            var sortedItemsResult = await _httpClient.GetAsync<SortedItemsResult>("info/items", headers: headers, QueryParameters: queryParameters);
            
            return sortedItemsResult.Data!;
        }

        public async Task<IEnumerable<ItemShort>> GetDetailsAsync(string language = "ru")
        {
            var headers = new Dictionary<string, string> { { "language", language } };

            var enumerable = await _httpClient.GetAsync<IEnumerable<ItemShort>>("info/details", headers: headers);

            return enumerable.Data!;
        }

        public async Task<BestDucatOffersResult> GetBestDucatTradesAsync(string language = "ru", int count = 100)
        {
            var headers = new Dictionary<string, string> { { "language", language } };
            var queryParameters = new Dictionary<string, string> { { "count", count.ToString() } };
            var ducatTrades = await _httpClient.GetAsync<BestDucatOffersResult>($"info/ducats", headers: headers, QueryParameters: queryParameters);
           
            return ducatTrades.Data!;
        }

        public async Task<ItemShort> GetItemDetailsAsync(string itemId, string language = "ru")
        {
            var headers = new Dictionary<string, string> { { "language", language } };
            var link = $"info/details/{itemId}";
            var result = await _httpClient.GetAsync<ItemShort>(link, headers: headers);

            return result.Data!;
        }

        public async Task<BestEndoOffersResult> GetBestEndoOffersAsync(string language = "ru", int count = 100)
        {
            var headers = new Dictionary<string, string> { { "language", language } };
            var queryParameters = new Dictionary<string, string> { { "count", count.ToString() } };
            var endoTrades = await _httpClient.GetAsync<BestEndoOffersResult>($"info/endo", headers: headers, QueryParameters: queryParameters);

            return endoTrades.Data!;
        }
    }

}
