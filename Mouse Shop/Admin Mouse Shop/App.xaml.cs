using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Admin_Mouse_Shop;
using Admin_Mouse_Shop.Services;
using Admin_Mouse_Shop.Services.Classes;
using Admin_Mouse_Shop.Services.Interfaces;
using Admin_Mouse_Shop.View;
using Admin_Mouse_Shop.ViewModel;

namespace Admin_Mouse_Shop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; } = new();
        protected override void OnStartup(StartupEventArgs e) // virtual void OnStartup
        {
            Register();
            var mainViewModel = App.Container.GetInstance<MainViewModel>();
            mainViewModel.MainOpen();
            MainStartup();
        }

        private void Register()
        {
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<IMyNavigationService, NavigationService>();
            Container.RegisterSingleton<IItemsService, ItemsService>();

            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<AddViewModel>();
            Container.RegisterSingleton<ChangeViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var mainViewModel = App.Container.GetInstance<MainViewModel>();
            mainViewModel.MainClose();
            base.OnExit(e);
        }

        private void MainStartup()
        {
            MainView mainView = new();
            mainView.DataContext = Container.GetInstance<MainViewModel>();
            mainView.ShowDialog();
        }
    }
}
