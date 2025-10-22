namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient.Models
{
    public class BestEndoOffersResult
    {
        /// <summary>
        /// Список продавцов с наилучшим соотношением эндо к платине (максимальная эффективность)
        /// </summary>
        public List<SellerEndoOffer> BestByEfficiency { get; set; } = new();

        /// <summary>
        /// Список продавцов с наибольшим количеством эндо за трейд (максимальный объём)
        /// </summary>
        public List<SellerEndoOffer> BestByTotalEndo { get; set; } = new();

        /// <summary>
        /// Время генерации отчёта / выборки
        /// </summary>
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Язык, для которого был сформирован отчёт (например, "ru", "en")
        /// </summary>
        public string Language { get; set; } = "ru";
    }

    public class SellerEndoOffer
    {
        public string SellerName { get; set; } = null!;

        /// <summary>
        /// Все предметы продавца, которые могут быть проданы за платину и имеют эндо
        /// </summary>
        public List<SellerEndoItem> AllItems { get; set; } = new();

        /// <summary>
        /// Оптимальный набор предметов (до 6 слотов) для максимальной эффективности
        /// </summary>
        public List<SellerEndoItem> SelectedItems { get; set; } = new();

        /// <summary>
        /// Общие эндо за выбранные предметы
        /// </summary>
        public int TotalEndo => SelectedItems.Sum(x => x.TotalEndo);

        /// <summary>
        /// Общая стоимость в платине
        /// </summary>
        public int TotalPlatinum => SelectedItems.Sum(x => x.TotalPlatinum);

        /// <summary>
        /// Общее количество предметов, участвующих в трейде (до 6)
        /// </summary>
        public int TotalTradeQuantity => SelectedItems.Sum(x => x.QuantityInTrade);

        /// <summary>
        /// Эффективность набора — сколько эндо на 1 платину
        /// </summary>
        public double EndoPerPlatinum => SelectedItems.Sum(x => x.TotalEndo) / (double)SelectedItems.Sum(x => x.TotalPlatinum);
    }

    public class SellerEndoItem
    {
        public string ItemId { get; set; } = null!;
        public string ItemSlug { get; set; } = null!;
        public string? Name { get; set; }

        /// <summary>
        /// Эффективность предмета — сколько эндо на 1 платину
        /// </summary>
        public double EndoPerPlatinum { get; set; }

        /// <summary>
        /// Общие эндо, получаемые за выбранное количество предмета
        /// </summary>
        public int TotalEndo { get; set; }

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
