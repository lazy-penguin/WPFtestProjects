using CashDispenserModel;
using GalaSoft.MvvmLight;

namespace CashDispenserViewModel
{
    public sealed class StateViewModel : ViewModelBase
    {
        private string _state;
        public string State
        {
            get => _state;
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }

        public StateViewModel(CashDispenser cashDispenser)
        {
            State = cashDispenser.GetState();
        }
    }
}
