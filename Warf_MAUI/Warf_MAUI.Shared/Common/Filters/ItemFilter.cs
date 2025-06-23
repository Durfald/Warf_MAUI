using System.ComponentModel;
using Warf_MAUI.Shared.Common.Mock;

namespace Warf_MAUI.Shared.Common.Filters
{
    internal class ItemFilter
    {
        private string _nameQuery = string.Empty;
        internal void SetNameQuery(string query)
        { _nameQuery = query; }

        private int _buyValueStart = 0;
        [DisplayName("Стоимость покупки от")]
        internal int BuyValueStart
        {
            get => _buyValueStart;
            set
            {
                _buyValueStart = value;
                CallPropertyChanged();
            }
        }

        private int _buyValueEnd = -1;
        [DisplayName("до")]
        internal int BuyValueEnd
        {
            get => _buyValueEnd;
            set
            {
                _buyValueEnd = value;
                CallPropertyChanged();
            }
        }

        private int _sellValueStart = 0;
        [DisplayName("Стоимость продажи от")]
        internal int SellValueStart
        {
            get => _sellValueStart;
            set
            {
                _sellValueStart = value;
                CallPropertyChanged();
            }
        }

        private int _sellValueEnd = -1;
        [DisplayName("до")]
        internal int SellValueEnd
        {
            get => _sellValueEnd;
            set
            {
                _sellValueEnd = value;
                CallPropertyChanged();
            }
        }

        private int _profitStart = 0;
        [DisplayName("Прибыль от")]
        internal int ProfitStart
        {
            get => _profitStart;
            set
            {
                _profitStart = value;
                CallPropertyChanged();
            }
        }

        private int _profitEnd = -1;
        [DisplayName("до")]
        internal int ProfitEnd
        {
            get => _profitEnd;
            set
            {
                _profitEnd = value;
                CallPropertyChanged();
            }
        }

        private int _rank = -1;
        [DisplayName("Ранг")]
        internal int Rank
        {
            get => _rank;
            set
            {
                _rank = value;
                CallPropertyChanged();
            }
        }

        private string _tax = string.Empty;
        [DisplayName("Налог")]
        internal string Tax
        {
            get => _tax;
            set
            {
                _tax = value;
                CallPropertyChanged();
            }
        }

        private int _trend = -1;
        [DisplayName("Тренд")]
        internal int Trend
        {
            get => _trend;
            set
            {
                _trend = value;
                CallPropertyChanged();
            }
        }

        [DisplayName("Тип метрики")]
        internal Metric MetricType { get; set; } = Metric.TwoDay;

        public enum Metric { TwoDay, ThreeMonth }

        internal event Action<string>? PropertyChanged;

        private void CallPropertyChanged()
        {
            PropertyChanged?.Invoke(_nameQuery);
        }

        internal static string GetNameForMetricType(Metric type)
        {
            return type switch
            {
                Metric.TwoDay => "Два дня",
                Metric.ThreeMonth => "Три месяца",
                _ => "NOT SET"
            };
        }

        internal static Metric GetValueOfMetricType(Metric type)
        {
            var metrics = Enum.GetValues<Metric>().ToList();
            var index = metrics.IndexOf(type);
            if (index == -1)
                return Metric.TwoDay;
            return metrics.ElementAt(index);
        }

        internal bool TrySetMetricByIndex(object? index)
        {
            if (index == null)
                return false;

            if (int.TryParse(index.ToString(), out var value))
            {
                var availableValues = Enum.GetValues<Metric>();
                if (availableValues.Length >= value)
                    return false;

                MetricType = availableValues[value];
                return true;
            }
            return false;
        }

        internal List<DemoItem> Apply(IQueryable<DemoItem> source)
        {
            if (!string.IsNullOrEmpty(Tax))
                source = source.Where(x => string.Compare(x.Tax, Tax) == 0);

            if (BuyValueStart > 0)
                source = source.Where(x => x.BuyValue >= BuyValueStart);
            if (BuyValueEnd > -1)
                source = source.Where(x => x.BuyValue <= BuyValueEnd);

            if (SellValueStart > 0)
                source = source.Where(x => x.SellValue >= SellValueStart);
            if (SellValueEnd > -1)
                source = source.Where(x => x.SellValue <= SellValueEnd);

            if (ProfitStart > 0)
                source = source.Where(x => x.Profit >= ProfitStart);
            if (ProfitEnd > -1)
                source = source.Where(x => x.Profit <= ProfitEnd);

            if (Rank > -1)
                source = source.Where(x => x.Rank == Rank);

            if (Trend > -1)
                source = source.Where(x => x.Trend == Trend);

            // todo: Я не понял что ты хочешь фильтровать в этой метрике.
            source = MetricType switch
            {
                Metric.ThreeMonth => source.Where(x => x.ThreeMonthMetric != -1),
                _ => source.Where(x => x.ThreeMonthMetric != -1), // 2 дня - дефолт
            };

            return [.. source];
        }
    }
}
