using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToDoList.View;
using ToDoList.ViewModel;

namespace ToDoList
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
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<ListViewModel>();
            Container.RegisterSingleton<ItemViewModel>();
        }

        private void MainStartup()
        {
            MainView mainView = new();
            mainView.DataContext = Container.GetInstance<MainViewModel>();
            mainView.ShowDialog();
        }
    }
}
