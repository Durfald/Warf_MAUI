using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.Enums;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v2.DTO
{
    public class OrderFilter
    {
        public string Slug { get; set; } = string.Empty;
        public Platform? Platform { get; set; } = null;
        public string Language { get; set; } = "ru";
    }
}
