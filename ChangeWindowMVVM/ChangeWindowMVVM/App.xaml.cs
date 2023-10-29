using ChangeWindowMVVM.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace ChangeWindowMVVM
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
            MainStartup();
        }

        private void Register()
        {
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<FirstViewModel>();
            Container.RegisterSingleton<SecondViewModel>();
            Container.RegisterSingleton<ThirdViewModel>();
        }

        private void MainStartup()
        {
            MainWindow mainView = new();
            mainView.DataContext = Container.GetInstance<MainViewModel>();
            mainView.ShowDialog();
        }
    }
}
