using GalaSoft.MvvmLight;
using System.Windows.Input;
using CashDispenserModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CashDispenserViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly CashDispenser _cashDispenser;
        public MainViewModel()
        {
            _cashDispenser = new CashDispenser();

            BalanceCommand = new RelayCommand(GetBalance);
            PutCommand = new RelayCommand(PutMoney);
            WithdrawCommand = new RelayCommand(WithdrawMoney);
            StateCommand = new RelayCommand(GetState);
            BackCommand = new RelayCommand(Back);

            MessengerInstance.Register<NotificationMessage>(this, "end", EndWork);
        }

        private void EndWork(NotificationMessage obj)
        {
            Back();
        }

        public ICommand BalanceCommand { get; set; }
        public ICommand PutCommand { get; set; }
        public ICommand WithdrawCommand { get; set; }
        public ICommand StateCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private void GetBalance()
        {
            IsBackEnable = true;
            IsPutVisible = false;
            IsWithdrawVisible = false;

            CurrentContentViewModel = new BalanceViewModel(_cashDispenser);
        }

        private void PutMoney()
        {
            IsBackEnable = true;
            IsPutEnable = false;
            IsBalanceVisible = false;
            IsWithdrawVisible = false;

            CurrentContentViewModel = new PutViewModel(_cashDispenser);
        }

        private void WithdrawMoney()
        {
            IsBackEnable = true;
            IsWithdrawEnable = false;
            IsBalanceVisible = false;
            IsPutVisible = false;

            CurrentContentViewModel = new WithdrawViewModel(_cashDispenser);
        }

        private void GetState()
        {
            IsBackEnable = true;
            IsBalanceVisible = false;
            IsPutVisible = false;
            IsWithdrawVisible = false;

            CurrentContentViewModel = new StateViewModel(_cashDispenser);
        }

        private void Back()
        {
            IsBackEnable = false;
            IsPutEnable = true;
            IsWithdrawEnable = true;
            IsBalanceVisible = true;
            IsPutVisible = true;
            IsWithdrawVisible = true;

            CurrentContentViewModel = null;
        }

        private object _currentContentViewModel;
        public object CurrentContentViewModel
        {
            get => _currentContentViewModel;
            set
            {
                _currentContentViewModel = value;
                RaisePropertyChanged();
            }
        }


        private bool _isBalanceVisible = true;
        private bool _isPutVisible = true;
        private bool _isWithdrawVisible = true;
        private bool _isPutEnable = true;
        private bool _isWithdrawEnable = true;
        private bool _isBackEnable;

        public bool IsBalanceVisible
        {
            get => _isBalanceVisible;
            set
            {
                _isBalanceVisible = value;
                RaisePropertyChanged();
            }
        }
        public bool IsPutVisible
        {
            get => _isPutVisible;
            set
            {
                _isPutVisible = value;
                RaisePropertyChanged();
            }
        }
        public bool IsWithdrawVisible
        {
            get => _isWithdrawVisible;
            set
            {
                _isWithdrawVisible = value;
                RaisePropertyChanged();
            }
        }
        public bool IsPutEnable
        {
            get => _isPutEnable;
            set
            {
                _isPutEnable = value;
                RaisePropertyChanged();
            }
        }
        public bool IsWithdrawEnable
        {
            get => _isWithdrawEnable;
            set
            {
                _isWithdrawEnable = value;
                RaisePropertyChanged();
            }
        }
        public bool IsBackEnable
        {
            get => _isBackEnable;
            set
            {
                _isBackEnable = value;
                RaisePropertyChanged();
            }
        }
    }
}
