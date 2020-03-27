using CashDispenserModel;
using GalaSoft.MvvmLight;

namespace CashDispenserViewModel
{
    public sealed class BalanceViewModel : ViewModelBase
    {
        private CashDispenser _cashDispenser;
        public CashDispenser CashDispenser
        {
            get => _cashDispenser;
                set
            {
                _cashDispenser = value;
                RaisePropertyChanged();
            }
        }
        public BalanceViewModel(CashDispenser cashDispenser)
        {
            _cashDispenser = cashDispenser;
        }
    }
}
