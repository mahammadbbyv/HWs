using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMVVM.Message;
using ToDoListMVVM.Service.Interface;

namespace ToDoListMVVM.Service.Class
{
    class MyNavigationService : IMyNavigationService
    {
        private readonly IMessenger _messenger;
        public MyNavigationService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void NavigateTo<T>(object? data = null, bool isNew = false) where T : ViewModelBase
        {
            _messenger.Send(new NavigationMessage()
            {
                Data = typeof(T)
            });

            if (data != null)
                if(!isNew)
                    _messenger.Send(new ToDoInfoMessage()
                    {
                        Data = data
                    });
                else
                    _messenger.Send(new CreatedToDoMessage()
                    {
                        Data = data
                    });
        }
    }
}
