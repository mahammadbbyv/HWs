using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Whiteboard.Context;
using Whiteboard.Services.Classes;
using Whiteboard.Services.Interfaces;
using Whiteboard.View;
using Whiteboard.ViewModel;

namespace Whiteboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; } = new();


        protected override void OnStartup(StartupEventArgs e)
        {
            Register();
            MainStartup();
        }

        private void Register()
        {
            Container.RegisterSingleton<IMessenger, Messenger>();

            Container.RegisterSingleton<ISerializeService, SerializeService>();
            Container.RegisterSingleton<IMyNavigationService, MyNavigationService>();
            Container.RegisterSingleton<IUserManageService, UserManageService>();
            Container.RegisterSingleton<IProjectManageService, ProjectManageService>();

            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<LoginViewModel>();
            Container.RegisterSingleton<RegisterViewModel>();
            Container.RegisterSingleton<HomeViewModel>();
            Container.RegisterSingleton<WhiteboardViewModel>();


            Container.RegisterSingleton<WhiteboardDbContext>();
            
        }

        private void MainStartup()
        {
            var mainView = new MainView();
            mainView.DataContext = Container.GetInstance<MainViewModel>();
            mainView.ShowDialog();
        }
    }
}
