using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.WebAPI.Clients
{
    internal interface IHttpClient
    {
        Task<T?> GetAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null);
        Task<T?> PostAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null);
        Task<T?> PutAsync<T>(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null);
        Task<T?> PatchAsync<T>(string endpoint, object body, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null);
        Task<bool> DeleteAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null, Dictionary<string, string>? QueryParameters = null);
    }
}
