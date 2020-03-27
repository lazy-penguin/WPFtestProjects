using GalaSoft.MvvmLight;

namespace TextProcessing
{
    public sealed class InputData : ObservableObject
    {
        private string _inputFileName;
        private string _outputFileName;
        private int _wordThreshold;
        private bool _deletePunctuation;

        public string InputFileName
        {
            get => _inputFileName;
            set
            {
                Set(() => InputFileName, ref _inputFileName, value);
            }
        }
        public string OutputFileName
        {
            get => _outputFileName;
            set
            {
                Set(() => OutputFileName, ref _outputFileName, value);
            }
        }
        public int WordThreshold
        {
            get => _wordThreshold;
            set
            {
                Set(() => WordThreshold, ref _wordThreshold, value);
            }
        }
        public bool DeletePunctuation
        {
            get => _deletePunctuation;
            set
            {
                Set(() => DeletePunctuation, ref _deletePunctuation, value);
            }
        }
    }
}
