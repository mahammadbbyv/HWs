using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Mouse_Shop.Messages;
using System.Net.Sockets;
using System.IO;

namespace Mouse_Shop.ViewModel
{
    class WindowViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                Set(ref _currentViewModel, value);
            }
        }
        public void ReceiveMessage(NavigationMessage message)
        {
            CurrentViewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
        }

        public WindowViewModel(IMessenger messenger)
        {
            if (!File.Exists("current_user.json")) { CurrentViewModel = App.Container.GetInstance<AuthorizationViewModel>(); }
            else { CurrentViewModel = App.Container.GetInstance<StoreViewModel>(); }
            
            _messenger = messenger;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);
        }
    }
}
