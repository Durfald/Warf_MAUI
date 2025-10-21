using Warf_MAUI.Shared.Common.BM25;

namespace Warf_MAUI.Shared.Common.Mock;
public class DemoItem : IBM25Item
{
    public string Name { get; init; } = null!;
    public int BuyValue { get; init; }
    public int SellValue { get; init; }
    public int Profit { get; init; }
    public int Rank { get; init; }
    public string Tax { get; init; } = null!;
    public int Trend { get; init; }
    public int ThreeMonthMetric { get; init; }
    public int TwoDayMetric { get; init; }
}
