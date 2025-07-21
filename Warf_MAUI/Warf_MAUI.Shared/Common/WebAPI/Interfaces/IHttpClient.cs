using Warf_MAUI.Shared.Common.WebAPI.WebClient;

namespace Warf_MAUI.Shared.Common.WebAPI.Interfaces
{
    public interface IHttpClient
    {
        Task<ApiResponse<T>> GetAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null) where T : class;
        Task<ApiResponse<T>> PostAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null) where T : class;
        Task<ApiResponse<T>> PutAsync<T>(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null) where T : class;
        Task<ApiResponse<T>> PatchAsync<T>(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null) where T : class;
        Task<bool> DeleteAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null);
        Task<ApiResponse> GetAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null);
        Task<ApiResponse> PostAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null);
        Task<ApiResponse> PutAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null);
        Task<ApiResponse> PatchAsync(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null, Dictionary<string, string>? cookies = null);
    }
    public interface IHttpClientMy : IHttpClient { }
    public interface IHttpClientV1 : IHttpClient { }
    public interface IHttpClientV2 : IHttpClient { }
}
