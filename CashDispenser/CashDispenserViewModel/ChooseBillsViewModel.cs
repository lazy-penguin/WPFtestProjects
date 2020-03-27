using System.Collections.ObjectModel;
using BillsDeclaration;
using GalaSoft.MvvmLight;

namespace CashDispenserViewModel
{
    public sealed class ChooseBillsViewModel : ViewModelBase
    {
        public ObservableCollection<BillViewModel> BillsView { get; set; }
        public ChooseBillsViewModel(int amountOfMoney)
        {
            BillsView = new ObservableCollection<BillViewModel>();
            foreach (var denomination in BillsDenominations.Denominations)
            {
                if (denomination > amountOfMoney)
                {
                    return;
                }

                var billViewModel = new BillViewModel
                {
                    BillCount = 0,
                    Denomination = denomination
                };
                BillsView.Add(billViewModel);
            }
        }
    }
}
