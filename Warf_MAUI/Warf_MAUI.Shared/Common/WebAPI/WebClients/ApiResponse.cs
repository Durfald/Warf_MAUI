using System.Net;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClient
{
    public class ApiResponse<T> where T : class
    {
        public T? Data { get; set; }
        public CookieCollection Cookies { get; set; } = new CookieCollection();
        public HttpStatusCode StatusCode { get; set; }
        public string? RawContent { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode < 300 && Data != null;
    }

    public class ApiResponse
    {
        public CookieCollection Cookies { get; set; } = new CookieCollection();
        public HttpStatusCode StatusCode { get; set; }
        public string? RawContent { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode < 300;
    }
}
