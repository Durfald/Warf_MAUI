namespace Warf_MAUI.Shared.Common.Mock
{
    internal static class MockItemList
    {
        internal static string FormatNumber(double value)
        {
            if (value >= 1_000_000)
                return $"{value / 1_000_000:0.#}M";
            if (value >= 1_000)
                return $"{value / 1_000:0.#}K";
            return value.ToString("0");
        }

        internal static List<DemoItem> GenerateDemoItems(int count)
        {
            List<DemoItem> items = [];
            for (int i = 0; i < count; i++)
            {
                int buyVal = Random.Shared.Next(1, 1000);
                var sellVal = Random.Shared.Next(1, 1000);

                items.Add(new DemoItem
                {
                    Name = RofloNameGenerator.GenerateRofloName(),
                    BuyValue = buyVal,
                    SellValue = sellVal,
                    Rank = Random.Shared.Next(0, 10),
                    Tax = FormatNumber(Random.Shared.Next(1, 100_000_000)),
                    Profit = sellVal - buyVal,
                    // Trend = ,
                    // ThreeMonthMetric = ,
                    // TwoDayMetric =,
                });
            }
            return items;
        }
    }
}
