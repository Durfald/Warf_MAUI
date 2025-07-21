using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v1;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Services
{
    public interface IAuthService
    {
        Task<string> GetCSRFTokenAsync();

        Task<User> GetJwtTokenAsync(string email, string password);
    }
}
