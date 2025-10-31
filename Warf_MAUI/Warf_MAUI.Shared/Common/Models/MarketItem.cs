using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.Common.Models
{
    using System.ComponentModel;

    public class MarketItem : INotifyPropertyChanged
    {
        public string OrderId { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        private string _name = null!;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        private string? _subtype;
        public string? Subtype
        {
            get => _subtype;
            set
            {
                if (_subtype != value)
                {
                    _subtype = value;
                    OnPropertyChanged(nameof(Subtype));
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        private int? _modRank;
        public int? ModRank
        {
            get => _modRank;
            set
            {
                if (_modRank != value)
                {
                    _modRank = value;
                    OnPropertyChanged(nameof(ModRank));
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string FullName =>
            Subtype != null ? $"{Name} {Subtype}" :
            ModRank != null ? $"{Name} {ModRank}" :
            Name;

        private int _minSellPriceOnMarket;
        public int MinSellPriceOnMarket
        {
            get => _minSellPriceOnMarket;
            set
            {
                if (_minSellPriceOnMarket != value)
                {
                    _minSellPriceOnMarket = value;
                    OnPropertyChanged(nameof(MinSellPriceOnMarket));
                    UpdateDifference();
                }
            }
        }

        private int _currentBuyPrice;
        public int CurrentBuyPrice
        {
            get => _currentBuyPrice;
            set
            {
                if (_currentBuyPrice != value)
                {
                    _currentBuyPrice = value;
                    OnPropertyChanged(nameof(CurrentBuyPrice));
                    UpdateDifference();
                }
            }
        }

        private int _currentSellPrice;
        public int CurrentSellPrice
        {
            get => _currentSellPrice;
            set
            {
                if (_currentSellPrice != value)
                {
                    _currentSellPrice = value;
                    OnPropertyChanged(nameof(CurrentSellPrice));
                }
            }
        }

        private int _difference;
        public int Difference
        {
            get => _difference;
            private set
            {
                if (_difference != value)
                {
                    _difference = value;
                    OnPropertyChanged(nameof(Difference));
                }
            }
        }

        private void UpdateDifference()
        {
            Difference = MinSellPriceOnMarket - CurrentBuyPrice;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
