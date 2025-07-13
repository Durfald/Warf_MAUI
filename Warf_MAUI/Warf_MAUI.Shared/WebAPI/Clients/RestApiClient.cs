using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.WebAPI.Clients
{
    internal class RestApiClient : IHttpClient
    {
        #region Private
        private readonly RestClient _client;

        private readonly Channel<bool> _rateLimitChannel = Channel.CreateBounded<bool>(new BoundedChannelOptions(3)
        {
            FullMode = BoundedChannelFullMode.Wait
        });

        private void StartRateLimiter()
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        await _rateLimitChannel.Writer.WriteAsync(true);
                    }
                    await Task.Delay(1000); // Ждём 1 секунду
                }
            });
        }
        #endregion

        public RestApiClient()
        {
            StartRateLimiter();
        }

        public RestApiClient(string baseUrl) : this()
        {
            _client = new RestClient(baseUrl);
        }

        private async Task<T?> ExecuteRequest<T>(RestRequest request)
        {
            await _rateLimitChannel.Reader.ReadAsync(); // 💡 Ждём слот
            var stopwatch = Stopwatch.StartNew();
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(2000); // ⏳ подожди 2 секунд
                return await ExecuteRequest<T>(request); // 🔁 повтор запроса
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                return default;
            }
            if (response.Content == null)
            {
                return default;
            }
            var data = JsonConvert.DeserializeObject<T>(response.Content);
            if (data == null)
            {
                return default;
            }
            return data;
        }

        #region Public
        public async Task<bool> DeleteAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null)
        {
            var request = new RestRequest(endpoint, Method.Delete);

            if (body != null)
                request.AddJsonBody(body);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            if (QueryParameters != null)
            {
                foreach (var queryParam in QueryParameters)
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
            }

            var response = await _client.ExecuteAsync(request);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<T?> GetAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null)
        {
            var request = new RestRequest(endpoint, Method.Get);

            if (body != null)
                request.AddJsonBody(body);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            if (QueryParameters != null)
            {
                foreach (var queryParam in QueryParameters)
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
            }

            return await ExecuteRequest<T>(request);
        }

        public async Task<T?> PatchAsync<T>(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null)
        {
            var request = new RestRequest(endpoint, Method.Patch);

            if (body != null)
                request.AddJsonBody(body);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            if (QueryParameters != null)
            {
                foreach (var queryParam in QueryParameters)
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
            }

            return await ExecuteRequest<T>(request);
        }

        public async Task<T?> PostAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null)
        {
            var request = new RestRequest(endpoint, Method.Post);

            if (body != null)
                request.AddJsonBody(body);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            if (QueryParameters != null)
            {
                foreach (var queryParam in QueryParameters)
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
            }

            return await ExecuteRequest<T>(request);
        }

        public async Task<T?> PutAsync<T>(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null)
        {
            var request = new RestRequest(endpoint, Method.Put);

            if (body != null)
                request.AddJsonBody(body);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            if (QueryParameters != null)
            {
                foreach (var queryParam in QueryParameters)
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
            }

            return await ExecuteRequest<T>(request);
        }
        #endregion
    }
}