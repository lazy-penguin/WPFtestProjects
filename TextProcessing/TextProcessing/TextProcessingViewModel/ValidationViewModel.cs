using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace TextProcessingViewModel
{
    public abstract class ValidationViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _error;
        public string Error
        {
            get => _error;
            private set
            {
                Set(() => Error, ref _error, value);
            }
        }

        public string this[string columnName] => Error = Validate(columnName);
        protected abstract string Validate(string columnName);
    }
}
