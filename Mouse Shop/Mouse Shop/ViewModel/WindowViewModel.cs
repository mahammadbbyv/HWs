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

namespace Mouse_Shop.ViewModel
{
    internal class WindowViewModel : ViewModelBase
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
            CurrentViewModel = App.Container.GetInstance<AuthorizationViewModel>();
            _messenger = messenger;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);
        }
    }
}
