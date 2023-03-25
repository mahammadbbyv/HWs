using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouse_Shop.Messages;

namespace Mouse_Shop.Services.Classes
{
    internal class NavigationService : IMyNavigationService
    {
        private readonly IMessenger _messenger;

        public NavigationService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void SendData<T>(object data) where T : ViewModelBase
        {
            _messenger.Send(new DataMessage()
            {
                Data = data
            });
        }

        public void NavigateTo<T>(object? data = null) where T : ViewModelBase
        {
            _messenger.Send(new NavigationMessage()
            {
                ViewModelType = typeof(T)
            });
        }
    }
}
