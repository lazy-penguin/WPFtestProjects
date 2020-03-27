using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace CashDispenserGUI
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

        public void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
