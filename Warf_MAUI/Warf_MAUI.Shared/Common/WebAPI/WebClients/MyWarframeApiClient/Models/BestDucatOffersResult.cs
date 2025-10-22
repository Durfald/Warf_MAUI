namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models
{
    public class BestDucatOffersResult
    {
        /// <summary>
        /// Список продавцов с наилучшим соотношением дукатов к платине (максимальная эффективность)
        /// </summary>
        public List<SellerDucatOffer> BestByEfficiency { get; set; } = new();

        /// <summary>
        /// Список продавцов с наибольшим количеством дукатов за трейд (максимальный объём)
        /// </summary>
        public List<SellerDucatOffer> BestByTotalDucats { get; set; } = new();

        /// <summary>
        /// Время генерации отчёта / выборки
        /// </summary>
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Язык, для которого был сформирован отчёт (например, "ru", "en")
        /// </summary>
        public string Language { get; set; } = "ru";
    }

    public class SellerDucatOffer
    {
        public string SellerName { get; set; } = null!;

        /// <summary>
        /// Все предметы продавца, которые могут быть проданы за платину и имеют дукаты
        /// </summary>
        public List<SellerDucatItem> AllItems { get; set; } = new();

        /// <summary>
        /// Оптимальный набор предметов (до 6 слотов) для максимальной эффективности
        /// </summary>
        public List<SellerDucatItem> SelectedItems { get; set; } = new();

        /// <summary>
        /// Общие дукаты за выбранные предметы
        /// </summary>
        public int TotalDucats => SelectedItems.Sum(x => x.TotalDucats);

        /// <summary>
        /// Общая стоимость в платине
        /// </summary>
        public int TotalPlatinum => SelectedItems.Sum(x => x.TotalPlatinum);

        /// <summary>
        /// Общее количество предметов, участвующих в трейде (до 6)
        /// </summary>
        public int TotalTradeQuantity => SelectedItems.Sum(x => x.QuantityInTrade);

        /// <summary>
        /// Эффективность набора — сколько дукатов на 1 платину
        /// </summary>
        public double DucatsPerPlatinum => SelectedItems.Sum(x => x.TotalDucats) / (double)SelectedItems.Sum(x => x.TotalPlatinum);
    }

    public class SellerDucatItem
    {
        public string ItemId { get; set; } = null!;
        public string ItemSlug { get; set; } = null!;
        public string? Name { get; set; }

        /// <summary>
        /// Эффективность предмета — сколько дукатов на 1 платину
        /// </summary>
        public double DucatsPerPlatinum { get; set; }

        /// <summary>
        /// Общие дукаты, получаемые за выбранное количество предмета
        /// </summary>
        public int TotalDucats { get; set; }

        /// <summary>
        /// Цена за единицу в платине
        /// </summary>
        public int Platinum { get; set; }

        /// <summary>
        /// Общая цена за выбранное количество
        /// </summary>
        public int TotalPlatinum { get; set; }

        /// <summary>
        /// Количество предметов в текущем трейде (до 6)
        /// </summary>
        public int QuantityInTrade { get; set; }

        /// <summary>
        /// Всего у продавца доступно
        /// </summary>
        public int TotalAvailableQuantity { get; set; }
    }
}
