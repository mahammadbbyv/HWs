using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMVVM.Message;
using ToDoListMVVM.Model;
using ToDoListMVVM.Service.Interface;

namespace ToDoListMVVM.ViewModel
{
    class ToDoVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMyNavigationService _navigationService;
        public ToDoItem ToDoItem { get; set; }
        public void RecieveMessage(ToDoInfoMessage message)
        {
            ToDoItem = message.Data as ToDoItem;
        }

        public ToDoVM(IMessenger messenger, IMyNavigationService navigationService)
        {
            _messenger = messenger;
            _navigationService = navigationService;
            _messenger.Register<ToDoInfoMessage>(this, RecieveMessage);
        }
        
        public RelayCommand BackCommand
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<ToDoListVM>();
            });
        }
    }
}
