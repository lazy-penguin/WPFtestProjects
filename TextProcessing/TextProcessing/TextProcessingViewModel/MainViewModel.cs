using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using TextProcessing;

namespace TextProcessingViewModel
{
    public class MainViewModel : ValidationViewModel
    {
        private TextProcessor _textProcessor;
        private readonly IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            InputData = new InputData();
            TasksViewModel = new TasksViewModel();

            OpenInputFileCommand = new RelayCommand(OpenInputFile);
            OpenOutputFileCommand = new RelayCommand(OpenOutputFile);
            ProcessCommand = new RelayCommand(Start);
            NewInputCommand = new RelayCommand(NewInput);
        }

        public ICommand OpenInputFileCommand { get; set; }
        public ICommand OpenOutputFileCommand { get; set; }
        public ICommand ProcessCommand { get; set; }
        public ICommand NewInputCommand { get; set; }

        private void OpenInputFile()
        {
            if (_dialogService.OpenFileDialog())       
            {
                InputData.InputFileName = _dialogService.FilePath;
            }
        }
        private void OpenOutputFile()
        {
            if (_dialogService.OpenFileDialog())
            {
                InputData.OutputFileName = _dialogService.FilePath;
            }
        }

        private async void Start()
        {
            if (InputData.InputFileName == null || InputData.OutputFileName == null)
            {
                Messenger.Default.Send(new NotificationMessage("Files field are empty!"), "error");
                return;
            }

            if (InputData.InputFileName.Equals(InputData.OutputFileName))
            {
                Messenger.Default.Send(new NotificationMessage("Output file can not match with input file!"), "error");
                return;
            }

            foreach (var task in _tasksViewModel.Tasks)
            {
                if (task.OutputFileName.Equals(InputData.OutputFileName))
                {
                    Messenger.Default.Send(new NotificationMessage("File already in use!"), "error");
                    return;
                }
            }

            IsButtonsEnable = false;
            InputData.WordThreshold = int.Parse(WordThreshold);
            _tasksViewModel.AddTask(InputData);

            var response = await Process(InputData);
            
            _tasksViewModel.RemoveTask(response);
            Messenger.Default.Send(new NotificationMessage($"File {response.OutputFileName} is ready!"), "info");
        }

        private async Task<InputData> Process(InputData inputData)
        {
            _textProcessor = new TextProcessor(inputData);
            await Task.Run(() => _textProcessor.Run());
            return _textProcessor.InputData;
        }

        private void NewInput()
        {
            IsButtonsEnable = true;
            InputData = new InputData();
            WordThreshold = string.Empty;
        }

        protected override string Validate(string columnName)
        {
            if (columnName == nameof(WordThreshold))
            {
                if (string.IsNullOrWhiteSpace(WordThreshold))
                {
                    return "This field can not be empty!";
                }
                if (!int.TryParse(WordThreshold, out _))
                {
                    return "This field must be a number!";
                }
                if (int.Parse(WordThreshold) <= 0)
                {
                    return "The number must be greater than 0!";
                }
            }
            return string.Empty;
        }

        private string _wordThreshold;
        public string WordThreshold
        {
            get => _wordThreshold;
            set
            {
                _wordThreshold = value;
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

        private InputData _inputData;
        public InputData InputData
        {
            get => _inputData;
            set
            {
                _inputData = value;
                RaisePropertyChanged();
            }
        }

        private TasksViewModel _tasksViewModel;
        public TasksViewModel TasksViewModel
        {
            get => _tasksViewModel;
            set
            {
                _tasksViewModel = value;
                RaisePropertyChanged();
            }
        }
    }
}

