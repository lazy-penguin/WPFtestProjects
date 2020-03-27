using Microsoft.Win32;
using TextProcessingViewModel;

namespace TextProcessingGUI
{
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; }
        public bool OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text documents (*.txt)|*.txt",
            };

            if (openFileDialog.ShowDialog() != true)
            {
                return false;
            }
            FilePath = openFileDialog.FileName;
            return true;
        }
    }
}
