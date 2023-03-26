using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouse_Shop.Messages;
using Mouse_Shop.Model;
using Mouse_Shop.ViewModel;

namespace Mouse_Shop.Services.Classes
{
    internal class NavigationService : IMyNavigationService
    {
        private readonly IMessenger _messenger;

        public NavigationService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void SendData<T>(object data, bool negative = false) where T : ViewModelBase
        {
            _messenger.Send(new DataMessage()
            {
                Data = data,
                IsNegative = negative
            });
        }

        public void SendProduct<T>(object data) where T : ViewModelBase
        {
            _messenger.Send(new BasketMessage()
            {
                product = data
            });
        }

        public void NavigateTo<T>(object? data = null) where T : ViewModelBase
        {
            _messenger.Send(new NavigationMessage()
            {
                ViewModelType = typeof(T)
            });

            if (data != null)
                SendData<T>(data);
        }
    }
}
