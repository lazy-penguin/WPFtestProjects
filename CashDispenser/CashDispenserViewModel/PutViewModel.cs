using System.Collections.ObjectModel;
using System.Windows.Input;
using BillsDeclaration;
using CashDispenserModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CashDispenserViewModel
{
    public sealed class PutViewModel : ViewModelBase
    {
        public ObservableCollection<BillViewModel> BillsView { get; set; }

        private readonly CashDispenser _cashDispenser;
        public PutViewModel(CashDispenser cashDispenser)
        {
            BillsView = new ObservableCollection<BillViewModel>();
            foreach (var denomination in BillsDenominations.Denominations)
            {
                var billViewModel = new BillViewModel
                {
                    BillCount = 0,
                    Denomination = denomination
                };
                BillsView.Add(billViewModel);
            }

            PutCommand = new RelayCommand(PutMoney);
            _cashDispenser = cashDispenser;
            MessengerInstance.Register<NotificationMessage<int>>(this, "add", BillCountChangeNotify);
        }

        private void BillCountChangeNotify(NotificationMessage<int> obj)
        {
            AmountOfMoney += obj.Content;
        }

        private int _amountOfMoney;
        public int AmountOfMoney
        {
            get => _amountOfMoney;
            set
            {
                _amountOfMoney = value;
                RaisePropertyChanged();
            }
        }

        public ICommand PutCommand { get; set; }

        public void PutMoney()
        {
            if (AmountOfMoney <= 0)
            {
                MessengerInstance.Send("Nothing to put!", "error");
                return;
            }

            var billsCount = new int[BillsView.Count];
            for (var i = 0; i < billsCount.Length; i++)
            {
                billsCount[i] = BillsView[i].BillCount;
            }

            var result = _cashDispenser.PutMoney(AmountOfMoney, billsCount);
            if (result == 0)
            {
                MessengerInstance.Send(new NotificationMessage($"You put {AmountOfMoney}!"), "info");
            }
            else
            {
                MessengerInstance.Send(new NotificationMessage("Sorry, cash dispenser reached the maximum number of bills: " +
                $"Still have place for {result} bills"), "error");
            }
            MessengerInstance.Send(new NotificationMessage("work ended"),"end");
        }
    }
}
