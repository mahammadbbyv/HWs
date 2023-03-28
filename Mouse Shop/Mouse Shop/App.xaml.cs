using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Mouse_Shop.Services;
using Mouse_Shop.Services.Classes;
using Mouse_Shop.Services.Interfaces;
using Mouse_Shop.View;
using Mouse_Shop.ViewModel;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mouse_Shop
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
            var windowviewmodel = App.Container.GetInstance<ProductsViewModel>();
            windowviewmodel.MainOpen();
            MainStartup();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var mainViewModel = App.Container.GetInstance<ProductsViewModel>();
            mainViewModel.MainClose();
            base.OnExit(e);
        }

        private void Register()
        {
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<IMyNavigationService, NavigationService>();
            Container.RegisterSingleton<IAuthorizationService, AuthorizationService>();
            Container.RegisterSingleton<ISerializeService, SerializeService>();
            Container.RegisterSingleton<IVerificationService, VerificationService>();
            Container.RegisterSingleton<IBasketService, BasketService>();
            Container.RegisterSingleton<IPurchaseService, PurchaseService>();
            Container.RegisterSingleton<ISortService, SortService>();

            Container.RegisterSingleton<WindowViewModel>();
            Container.RegisterSingleton<AuthorizationViewModel>();
            Container.RegisterSingleton<VerifyViewModel>();
            Container.RegisterSingleton<StoreViewModel>();
            Container.RegisterSingleton<ProductsViewModel>();
            Container.RegisterSingleton<CartViewModel>();
        }

        private void MainStartup()
        {
            WindowView mainView = new();
            mainView.DataContext = Container.GetInstance<WindowViewModel>();
            mainView.ShowDialog();
        }
    }
}
