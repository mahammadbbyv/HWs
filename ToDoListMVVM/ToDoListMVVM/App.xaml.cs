using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ToDoListMVVM.Model;
using ToDoListMVVM.Service.Class;
using ToDoListMVVM.Service.Interface;
using ToDoListMVVM.View;
using ToDoListMVVM.ViewModel;

namespace ToDoListMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        public static Container Container { get; set; } = new();
        //public static ObservableCollection<ToDoItem> Items { get; set; } = new();
        protected override void OnStartup(StartupEventArgs e)
        {
            Register();
            MainStartup();
        }

        private void Register()
        {
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<IMyNavigationService, MyNavigationService>();

            Container.Register<WindowVM>();
            Container.Register<ToDoListVM>();
            Container.Register<ToDoVM>();
            Container.Register<AddVM>();
        }

        private void MainStartup()
        {
            WindowView mainView = new();
            mainView.DataContext = Container.GetInstance<WindowVM>();
            mainView.ShowDialog();
        }
    }

    
}
