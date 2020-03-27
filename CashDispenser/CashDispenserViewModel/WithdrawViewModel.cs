using System.Windows.Input;
using BillsDeclaration;
using CashDispenserModel;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CashDispenserViewModel
{
    public sealed class WithdrawViewModel : ValidationViewModel
    {
        private int _denomination;

        public WithdrawViewModel(CashDispenser cashDispenser)
        {
            WithdrawCommand = new RelayCommand(WithdrawMoney);
            ChooseBillsCommand = new RelayCommand(ChooseBills);

            MessengerInstance.Register<NotificationMessage<int>>(this, "choose", BillChosenNotify);
            _cashDispenser = cashDispenser;
        }

        private void BillChosenNotify(NotificationMessage<int> obj)
        {
            _denomination = obj.Content;
        }

        public ICommand ChooseBillsCommand { get; set; }
        public ICommand WithdrawCommand { get; set; }

        private void ChooseBills()
        {
            if (BillsViewModel != null)
            {
                return;
            }
            IsSumReadOnly = true;
            BillsViewModel = new ChooseBillsViewModel(int.Parse(AmountOfMoney));
        }
        private void WithdrawMoney()
        {
            var result = _cashDispenser.WithdrawMoney(int.Parse(AmountOfMoney), _denomination);
            if (result == int.Parse(AmountOfMoney))
            {
                MessengerInstance.Send(new NotificationMessage($"You withdraw {AmountOfMoney}!"), "info");
            }
            else if (result == 0)
            {
                MessengerInstance.Send(new NotificationMessage("You can not withdraw so much money. Cash dispenser has not needed bills."), "error");
            }
            else
            {
                MessengerInstance.Send(new NotificationMessage($"You withdraw only {result}. Cash dispenser has not needed bills."), "info");
            }

            MessengerInstance.Send(new NotificationMessage("work ended"), "end");
            BillsViewModel = null;
        }

        protected override string Validate(string columnName)
        {
            if (columnName == nameof(AmountOfMoney))
            {
                IsButtonsEnable = false;
                if (string.IsNullOrWhiteSpace(AmountOfMoney))
                {
                    return "This field can not be empty!";
                }
                if (!int.TryParse(AmountOfMoney, out _))
                {
                    return "This field must be a number!";
                }
                if (int.Parse(AmountOfMoney) <= 0)
                {
                    return "Nothing to withdraw!";
                }
                if (int.Parse(AmountOfMoney) < BillsDenominations.Denominations[0])
                {
                    return "Cash dispenser has not suitable bills!";
                }
                if (int.Parse(AmountOfMoney) > _cashDispenser.Balance)
                {
                    return "Not enough money!";
                }
            }

            IsButtonsEnable = true;
            return string.Empty;
        }

        private string _amountOfMoney = "0";
        public string AmountOfMoney
        {
            get => _amountOfMoney;
            set
            {
                _amountOfMoney = value;
                RaisePropertyChanged();
            }
        }

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

        private ChooseBillsViewModel _billsViewModel;
        public ChooseBillsViewModel BillsViewModel
        {
            get => _billsViewModel;
            set
            {
                _billsViewModel = value;
                RaisePropertyChanged();
            }
        }

        private bool _isSumReadOnly;
        public bool IsSumReadOnly
        {
            get => _isSumReadOnly;
            set
            {
                _isSumReadOnly = value;
                RaisePropertyChanged();
            }
        }

        private bool _isButtonsEnable = true;
        public bool IsButtonsEnable
        {
            get => _isButtonsEnable;
            set
            {
                _isButtonsEnable = value;
                RaisePropertyChanged();
            }
        }

    }
}
