using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NoName
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();
        MainVM mainWindowViewModel;

        public App()
        {
            displayRootRegistry.RegisterWindowType<MainVM, MainWindow>();
            displayRootRegistry.RegisterWindowType<NotificationVM, Notification>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            mainWindowViewModel = new MainVM();
        
            await displayRootRegistry.ShowModalPresentation(mainWindowViewModel);
        
        }
        
    }
}
