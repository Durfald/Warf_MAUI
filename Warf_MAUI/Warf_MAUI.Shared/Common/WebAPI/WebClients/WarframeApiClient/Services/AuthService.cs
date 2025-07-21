using Warf_MAUI.Shared.Common.WebAPI.Interfaces;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v1;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientV1 _httpClient;
        public AuthService(IHttpClientV1 httpClient)
        {
            _httpClient = httpClient;
        }

        #region Authentication_v1

        public async Task<string> GetCSRFTokenAsync()
        {
            var response = await _httpClient.GetAsync("");
            return response.Cookies!["JWT"]!.Value;
        }

        public async Task<User> GetJwtTokenAsync(string email, string password)
        {
            var csrfToken = await GetCSRFTokenAsync();

            var resp = await _httpClient.PostAsync<User>("auth/signin",
                headers: new Dictionary<string, string>() { { "Authorization", $"Bearer {csrfToken}" } },
                body: new
                {
                    auth_type = "header",
                    password,
                    email,
                    device_id = "pc"
                });
            return resp.Data!;
        }

        #endregion

    }
}
