using System.ComponentModel;
using Warf_MAUI.Shared.Common.Mock;

namespace Warf_MAUI.Shared.Common.Filters
{
    /// <summary>
    /// Класс фильтрации предметов по различным параметрам, включая цену покупки/продажи, прибыль, ранг и т.д.
    /// </summary>
    internal class ItemFilter
    {
        private string _nameQuery = string.Empty;

        /// <summary>
        /// Устанавливает строку поиска по имени (не используется в фильтрации напрямую).
        /// </summary>
        internal void SetNameQuery(string query) => _nameQuery = query;

        private int _buyValueStart = 0;

        /// <summary>
        /// Нижняя граница стоимости покупки.
        /// </summary>
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

        /// <summary>
        /// Верхняя граница стоимости покупки.
        /// </summary>
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

        /// <summary>
        /// Нижняя граница стоимости продажи.
        /// </summary>
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

        /// <summary>
        /// Верхняя граница стоимости продажи.
        /// </summary>
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

        /// <summary>
        /// Нижняя граница прибыли.
        /// </summary>
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

        /// <summary>
        /// Верхняя граница прибыли.
        /// </summary>
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

        /// <summary>
        /// Точный фильтр по рангу (если задан).
        /// </summary>
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

        /// <summary>
        /// Фильтр по налоговой категории (точное совпадение).
        /// </summary>
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

        /// <summary>
        /// Фильтр по тренду (точное значение).
        /// </summary>
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

        /// <summary>
        /// Тип метрики, используемой для фильтрации (например, данные за 2 дня или за 3 месяца).
        /// </summary>
        [DisplayName("Тип метрики")]
        internal Metric MetricType { get; set; } = Metric.TwoDay;

        /// <summary>
        /// Возможные типы метрик.
        /// </summary>
        public enum Metric { TwoDay, ThreeMonth }

        /// <summary>
        /// Событие для уведомления об изменении свойства (передаёт _nameQuery).
        /// </summary>
        internal event Action<string>? PropertyChanged;

        /// <summary>
        /// Вызов события PropertyChanged.
        /// </summary>
        private void CallPropertyChanged()
        {
            PropertyChanged?.Invoke(_nameQuery);
        }

        /// <summary>
        /// Возвращает строковое представление значения метрики.
        /// </summary>
        internal static string GetNameForMetricType(Metric type)
        {
            return type switch
            {
                Metric.TwoDay => "Два дня",
                Metric.ThreeMonth => "Три месяца",
                _ => "NOT SET"
            };
        }

        /// <summary>
        /// Возвращает значение метрики по индексу в списке перечисления.
        /// </summary>
        internal static Metric GetValueOfMetricType(Metric type)
        {
            var metrics = Enum.GetValues<Metric>().ToList();
            var index = metrics.IndexOf(type);
            if (index == -1)
                return Metric.TwoDay;
            return metrics.ElementAt(index);
        }

        /// <summary>
        /// Устанавливает значение MetricType по индексу, если возможно.
        /// </summary>
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

        /// <summary>
        /// Применяет фильтры к источнику данных DemoItem.
        /// </summary>
        internal List<DemoItem> Apply(IQueryable<DemoItem> source)
        {
            // Фильтр по налогу
            if (!string.IsNullOrEmpty(Tax))
                source = source.Where(x => string.Compare(x.Tax, Tax) == 0);

            // Фильтры по стоимости покупки
            if (BuyValueStart > 0)
                source = source.Where(x => x.BuyValue >= BuyValueStart);
            if (BuyValueEnd > -1)
                source = source.Where(x => x.BuyValue <= BuyValueEnd);

            // Фильтры по стоимости продажи
            if (SellValueStart > 0)
                source = source.Where(x => x.SellValue >= SellValueStart);
            if (SellValueEnd > -1)
                source = source.Where(x => x.SellValue <= SellValueEnd);

            // Фильтры по прибыли
            if (ProfitStart > 0)
                source = source.Where(x => x.Profit >= ProfitStart);
            if (ProfitEnd > -1)
                source = source.Where(x => x.Profit <= ProfitEnd);

            // Фильтр по рангу
            if (Rank > -1)
                source = source.Where(x => x.Rank == Rank);

            // Фильтр по тренду
            if (Trend > -1)
                source = source.Where(x => x.Trend == Trend);

            // todo: Здесь можно добавить дополнительную логику фильтрации по значению метрики.
            // Сейчас оба варианта фильтруют по полю ThreeMonthMetric != -1
            source = MetricType switch
            {
                Metric.ThreeMonth => source.Where(x => x.ThreeMonthMetric != -1),
                _ => source.Where(x => x.ThreeMonthMetric != -1), // Заглушка для "2 дня"
            };

            return [.. source]; // Преобразует IQueryable в List
        }
    }
}
