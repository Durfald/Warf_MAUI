using Warf_MAUI.Shared.Common.BM25;

namespace Warf_MAUI.Shared.Common.Mock;
public class DemoItem : ISearchableItem
{
    public string Name { get; init; }
    public int BuyValue { get; init; }
    public int SellValue { get; init; }
    public int Profit { get; init; }
    public int Rank { get; init; }
    public string Tax { get; init; }
    public int Trend { get; init; }
    public int ThreeMonthMetric { get; init; }
    public int TwoDayMetric { get; init; }
}
