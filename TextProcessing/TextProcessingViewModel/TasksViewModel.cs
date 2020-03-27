using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using TextProcessing;

namespace TextProcessingViewModel
{
    public sealed class TasksViewModel : ViewModelBase
    {
        private bool _isListVisible;

        public bool IsListVisible
        {
            get => _isListVisible;
            set
            {
                _isListVisible = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<InputData> Tasks { get; set; }

        public TasksViewModel()
        {
            Tasks = new ObservableCollection<InputData>();
        }

        public void AddTask(InputData data)
        {
            if (!IsListVisible)
                IsListVisible = true;

            Tasks.Add(data);
            RaisePropertyChanged(() => Tasks);
        }

        public void RemoveTask(InputData data)
        {
            Tasks.Remove(data);
            if (Tasks.Count == 0)
            {
                IsListVisible = false;
            }
        }
    }
}
