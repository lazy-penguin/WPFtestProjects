using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace TextProcessingGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, "info", ShowInfoWindow);
            Messenger.Default.Register<NotificationMessage>(this, "error", ShowErrorWindow);
        }

        private void ShowInfoWindow(NotificationMessage message)
        {
            MessageBox.Show(message.Notification, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ShowErrorWindow(NotificationMessage message)
        {
            MessageBox.Show(message.Notification, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
