using Warf_MAUI.Shared.Common.WebAPI.Interfaces;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Service
{
    public class InfoService : IInfoService
    {
        private readonly IHttpClientMy _httpClient;
        //private readonly ILogger<InfoService> _logger;

        public InfoService(IHttpClientMy httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<StatusDto> GetStatusAsync(string language = "ru")
        {
            //_logger.LogInformation("Запрос статуса API с языком {Language}", language);
            var headers = new Dictionary<string, string> { { "language", language } };

            var statusDto = await _httpClient.GetAsync<StatusDto>("info/status", headers: headers);
            return statusDto.Data!;

            //var req = new HttpRequestMessage(HttpMethod.Get, "api/info/status");
            //req.Headers.Add("language", language);

            //var resp = await _httpClient.SendAsync(req);
            //if (!resp.IsSuccessStatusCode)
            //{
            //    //_logger.LogError("Ошибка при получении статуса: {StatusCode}", resp.StatusCode);
            //    throw new HttpRequestException($"Failed to get status: {resp.StatusCode}");
            //}

            //return await resp.Content.ReadFromJsonAsync<StatusDto>();
        }

        public async Task<SortedItemsResult> GetSortedItemsAsync(string language = "ru", int minSpread = 15, int minLiquidity = 60)
        {
            //_logger.LogInformation("Запрос сортированных предметов с языком {Language}, minSpread={minSpread}, minLiquidity={minLiquidity}", language, minSpread, minLiquidity);

            var headers = new Dictionary<string, string> { { "language", language } };

            var queryParameters = new Dictionary<string, string> { { "minSpread", minSpread.ToString() }, { "minLiquidity", minLiquidity.ToString() } };

            var sortedItemsResult = await _httpClient.GetAsync<SortedItemsResult>("info/items", headers: headers, QueryParameters: queryParameters);
            return sortedItemsResult.Data!;

            //var url = $"api/info/items?minSpread={minSpread}&minLiquidity={minLiquidity}";
            //var req = new HttpRequestMessage(HttpMethod.Get, url);
            //req.Headers.Add("language", language);

            //if (!resp.IsSuccessStatusCode)
            //{
            //    //_logger.LogError("Ошибка при получении предметов: {StatusCode}", resp.StatusCode);
            //    throw new HttpRequestException($"Failed to get sorted items: {resp.StatusCode}");
            //}

            //return await resp.Content.ReadFromJsonAsync<SortedItemsResult>();
        }

        public async Task<IEnumerable<ItemShort>> GetDetailsAsync(string language = "ru")
        {
            //_logger.LogInformation("Запрос деталей предметов с языком {Language}", language);

            var headers = new Dictionary<string, string> { { "language", language } };

            var enumerable = await _httpClient.GetAsync<IEnumerable<ItemShort>>("info/details", headers: headers);
            return enumerable.Data!;

            //var req = new HttpRequestMessage(HttpMethod.Get, "api/info/details");
            //req.Headers.Add("language", language);

            //var resp = await _httpClient.SendAsync(req);
            //if (!resp.IsSuccessStatusCode)
            //{
            //    //_logger.LogError("Ошибка при получении деталей предметов: {StatusCode}", resp.StatusCode);
            //    throw new HttpRequestException($"Failed to get item details: {resp.StatusCode}");
            //}

            //return await resp.Content.ReadFromJsonAsync<IEnumerable<Item>>();
        }

        public async Task<List<DucatTrade>> GetBestDucatTradesAsync(string language = "ru", int count = 100)
        {
            //_logger.LogInformation("Запрос лучших дукатных трейдов с языком {Language}, count={Count}", language, count);

            var headers = new Dictionary<string, string> { { "language", language } };
            var queryParameters = new Dictionary<string, string> { { "count", count.ToString() } };
            var ducatTrades = await _httpClient.GetAsync<List<DucatTrade>>($"info/ducats", headers: headers, QueryParameters: queryParameters);
            return ducatTrades.Data!;

            //var url = $"api/info/ducats?count={count}";
            //var req = new HttpRequestMessage(HttpMethod.Get, url);
            //req.Headers.Add("language", language);

            //var resp = await _httpClient.SendAsync(req);
            //if (!resp.IsSuccessStatusCode)
            //{
            //    //_logger.LogError("Ошибка при получении дукатных трейдов: {StatusCode}", resp.StatusCode);
            //    throw new HttpRequestException($"Failed to get ducat trades: {resp.StatusCode}");
            //}

            //return await resp.Content.ReadFromJsonAsync<List<DucatTrade>>();
        }

        public async Task<ItemShort> GetItemDetailsAsync(string itemId, string language = "ru")
        {
            var headers = new Dictionary<string, string> { { "language", language } };
            var link = $"info/details/{itemId}";
            var result = await _httpClient.GetAsync<ItemShort>(link, headers: headers);
            return result.Data!;

        }
    }

}
