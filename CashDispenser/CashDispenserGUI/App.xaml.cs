using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CashDispenserViewModel;

namespace CashDispenserGUI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    ///
    public partial class App : Application
    {
        private readonly MainViewModel _mainViewModel = new MainViewModel();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainWindow() { DataContext = _mainViewModel }.Show();
        }
    }
}
