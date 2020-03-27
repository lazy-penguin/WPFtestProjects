using GalaSoft.MvvmLight;
using System;
using System.Linq;
using BillsDeclaration;

namespace CashDispenserModel
{
    public sealed class CashDispenser : ObservableObject
    {
        private const int BillsCapacity = 100;
        private const int StartBillsCount = 5;

        private int _localBillsCount;
        private int _totalSum;
        private readonly int[] _billsCount;

        private int _balance;
        public int Balance
        {
            get => _balance;
            set
            {
                Set(() => Balance, ref _balance, value);
            }
        }

        public CashDispenser()
        {
            _billsCount = new int[BillsDenominations.Denominations.Length];
            _localBillsCount = BillsDenominations.Denominations.Length * StartBillsCount;
            for (var i = 0; i < _billsCount.Length; i++)
            {
                _billsCount[i] = StartBillsCount;
                _totalSum += BillsDenominations.Denominations[i] * StartBillsCount;
            }
        }

        public string GetState()
        {
            var result = "Cash dispenser has:\n" +
                         $"{BillsCapacity} bills capacity.\n" +
                         $"{_localBillsCount} bills at all worth {_totalSum}\n";

            for (var i = 0; i < BillsDenominations.Denominations.Length; i++)
            {
                result += $"{_billsCount[i]} bills denomination {BillsDenominations.Denominations[i]}\n";
            }

            return result;
        }

        public int PutMoney(int sum, int[] bills)
        {
            var billsCount = bills.Sum();
            if (billsCount + _localBillsCount > BillsCapacity)
            {
                return BillsCapacity - _localBillsCount;
            }

            PutBills(bills);
            _localBillsCount += billsCount;
            _totalSum += sum;
            Balance += sum;
            return 0;
        }
        private void PutBills(int[] newBills)
        {
            for (var i = _billsCount.Length - 1; i >=0; i--)
            {
                _billsCount[i] += newBills[i];
            }
        }

        public int WithdrawMoney(int sum, int denomination)
        {
            var rest = denomination == 0 ? WithdrawAvailableBills(sum) : WithdrawCertainBill(sum, denomination);

            var result = sum - rest;
            _totalSum -= result;
            Balance -= result;
            return result;
        }

        /*Withdraw as many needed bills as possible*/
        private int WithdrawCertainBill(int sum, int denomination)
        {
            var currentSum = sum;
            var index = Array.IndexOf(BillsDenominations.Denominations, denomination);
            while (currentSum > 0 && _billsCount[index] > 0 && currentSum >= BillsDenominations.Denominations[index])
            {
                currentSum -= BillsDenominations.Denominations[index];
                _billsCount[index]--;
            }

            if (currentSum > 0)
            {
                currentSum = WithdrawAvailableBills(currentSum);
            }
            return currentSum; 
        }

        private int WithdrawAvailableBills(int sum)
        {
            var currentSum = sum;
            for (var i = BillsDenominations.Denominations.Length - 1; i >= 0; i--)
            {
                while (currentSum > 0 && currentSum >= BillsDenominations.Denominations[i] && _billsCount[i] > 0)
                {
                    currentSum -= BillsDenominations.Denominations[i];
                    _billsCount[i]--;
                }
            }
            return currentSum;
        }
    }
}
