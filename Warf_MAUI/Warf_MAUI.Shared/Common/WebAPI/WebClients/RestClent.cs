using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Threading.Channels;
using Warf_MAUI.Shared.Common.WebAPI.Interfaces;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClient
{
    public class RestApiClient : IHttpClientV1, IHttpClientV2, IHttpClientMy
    {
        private readonly RestClient _client;
        private readonly int _maxRateLimit;

        private readonly Channel<bool> _rateLimitChannel = null!;
        private void StartRateLimiter()
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    for (int i = 0; i < _maxRateLimit; i++)
                    {
                        await _rateLimitChannel.Writer.WriteAsync(true);
                    }
                    await Task.Delay(1000); // Ждём 1 секунду
                }
            });
        }

        public RestApiClient(string baseUrl, int MaxRateLimit = 3)
        {
            _client = new RestClient(baseUrl);
            _maxRateLimit = MaxRateLimit;
            if (_maxRateLimit > 0)
            {
                _rateLimitChannel = Channel.CreateBounded<bool>(new BoundedChannelOptions(_maxRateLimit)
                 {
                     FullMode = BoundedChannelFullMode.Wait
                 });

                StartRateLimiter();
            }
        }

        private RestRequest PrepareRequest(string endpoint, Method method, object? body, Dictionary<string, string>? headers, Dictionary<string, string>? queryParams, Dictionary<string, string>? cookies)
        {
            var request = new RestRequest(endpoint, method);
            if (body != null) request.AddJsonBody(body);
            if (cookies != null) foreach (var c in cookies) request.AddCookie(c.Key, c.Value, "/", "warframe.market");
            if (headers != null) foreach (var h in headers) request.AddHeader(h.Key, h.Value);
            if (queryParams != null) foreach (var q in queryParams) request.AddQueryParameter(q.Key, q.Value);

            return request;
        }
        private async Task<ApiResponse<T>> ExecuteRequest<T>(RestRequest request, int attempt = 1, int maxAttempts = 5) where T : class
        {
            if(_maxRateLimit > 0)
                await _rateLimitChannel.Reader.ReadAsync();
            var response = await _client.ExecuteAsync(request);

            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Cookies = response.Cookies ?? new CookieCollection(),
                RawContent = response.Content
            };

            if (response.StatusCode != HttpStatusCode.OK && attempt < maxAttempts)
            {
                await Task.Delay(2000);
                return await ExecuteRequest<T>(request, attempt + 1, maxAttempts);
            }

            if (response.Content == null)
            {
                apiResponse.ErrorMessage = "Response content is null";
                return apiResponse;
            }

            try
            {
                apiResponse.Data = JsonConvert.DeserializeObject<T>(response.Content);
                if (apiResponse.Data == null)
                    apiResponse.ErrorMessage = $"Deserialization to {typeof(T).Name} failed";
            }
            catch (Exception ex)
            {
                apiResponse.ErrorMessage = $"Deserialization exception: {ex.Message}";
            }

            return apiResponse;
        }

        private async Task<ApiResponse> ExecuteRequest(RestRequest request, int attempt = 1, int maxAttempts = 5)
        {
            if (_maxRateLimit > 0)
                await _rateLimitChannel.Reader.ReadAsync();
            var response = await _client.ExecuteAsync(request);

            var apiResponse = new ApiResponse
            {
                StatusCode = response.StatusCode,
                Cookies = response.Cookies ?? new CookieCollection(),
                RawContent = response.Content
            };

            if (response.StatusCode != HttpStatusCode.OK && attempt < maxAttempts)
            {
                await Task.Delay(2000);
                return await ExecuteRequest(request, attempt + 1, maxAttempts);
            }

            return apiResponse;
        }

        public async Task<ApiResponse> GetAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null)
        {
            var request = PrepareRequest(endpoint, Method.Get, body, headers, queryParams, cookies);
            return await ExecuteRequest(request);
        }

        public async Task<ApiResponse> PostAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null)
        {
            var request = PrepareRequest(endpoint, Method.Post, body, headers, queryParams, cookies);
            return await ExecuteRequest(request);
        }

        public async Task<ApiResponse> PutAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null)
        {
            var request = PrepareRequest(endpoint, Method.Put, body, headers, queryParams, cookies);
            return await ExecuteRequest(request);
        }

        public async Task<ApiResponse> PatchAsync(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null)
        {
            var request = PrepareRequest(endpoint, Method.Patch, body, headers, queryParams, cookies);
            return await ExecuteRequest(request);
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null) where T : class
        {
            var request = PrepareRequest(endpoint, Method.Get, body, headers, queryParams, cookies);
            return await ExecuteRequest<T>(request);
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null) where T : class
        {
            var request = PrepareRequest(endpoint, Method.Post, body, headers, queryParams, cookies);
            return await ExecuteRequest<T>(request);
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null) where T : class
        {
            var request = PrepareRequest(endpoint, Method.Put, body, headers, queryParams, cookies);
            return await ExecuteRequest<T>(request);
        }

        public async Task<ApiResponse<T>> PatchAsync<T>(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null) where T : class
        {
            var request = PrepareRequest(endpoint, Method.Patch, body, headers, queryParams, cookies);
            return await ExecuteRequest<T>(request);
        }

        public async Task<bool> DeleteAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? cookies = null)
        {
            var request = PrepareRequest(endpoint, Method.Delete, body, headers, queryParams, cookies);
            var response = await ExecuteRequest(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

    }
}
