namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models
{
    public class DucatTrade
    {
        public string ItemId { get; set; } = null!;
        public string ItemSlug { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double DucatsPerPlatinum { get; set; }
        public int TotalDucats { get; set; }
        public int Platinum { get; set; }
        public int TotalPlatinum { get; set; }
        public int QuantityInTrade { get; set; } // До 6
        public int TotalAvailableQuantity { get; set; } // Всего у продавца
        public string SellerName { get; set; } = null!; // 🆕 Никнейм продавца
    }
}
