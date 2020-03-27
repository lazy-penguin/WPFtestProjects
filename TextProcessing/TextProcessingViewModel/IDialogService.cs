
namespace TextProcessingViewModel
{
    public interface IDialogService
    {
        string FilePath { get; set; }
        bool OpenFileDialog();
    }
}
