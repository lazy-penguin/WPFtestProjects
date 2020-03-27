using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CashDispenserViewModel
{
    public sealed class BillViewModel : ViewModelBase
    {
        public BillViewModel()
        {
            AddCommand = new RelayCommand(AddBills);
            ChooseCommand = new RelayCommand(ChooseBills);
            SubtractCommand = new RelayCommand(SubtractBills);
        }

        public ICommand AddCommand { get; set; }
        public ICommand ChooseCommand { get; set; }
        public ICommand SubtractCommand { get; set; }

        private int _billCount;
        public int BillCount
        {
            get => _billCount;
            set
            {
                _billCount = value;
                RaisePropertyChanged();
            }
        }

        private int _denomination;
        public int Denomination
        {
            get => _denomination;
            set
            {
                _denomination = value;
                RaisePropertyChanged();
            }
        }

        private void AddBills()
        {
            ++BillCount;
            MessengerInstance.Send(new NotificationMessage<int>(Denomination, "add"), "add");
        }

        private void ChooseBills()
        {
            MessengerInstance.Send(new NotificationMessage<int>(Denomination, "choose"), "choose");
        }
        private void SubtractBills()
        {
            if (BillCount > 0)
            {
                --BillCount;
                MessengerInstance.Send(new NotificationMessage<int>(-Denomination, "add"), "add");
            }
        }
    }
}
