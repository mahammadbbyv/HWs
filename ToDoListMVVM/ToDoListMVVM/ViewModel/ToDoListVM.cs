using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMVVM.Message;
using ToDoListMVVM.Model;
using ToDoListMVVM.Service.Interface;

namespace ToDoListMVVM.ViewModel
{
    class ToDoListVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMyNavigationService _navigationService;
        public static ObservableCollection<ToDoItem> Items { get; set; } = new();
        public RelayCommand<object> AddCommand
        {
            get => new(ToDo =>
            {
                _navigationService.NavigateTo<AddVM>();
            });
        }

        public RelayCommand<object> OpenToDoCommand
        {
            get => new(ToDo =>
            {
                ToDoItem item = (ToDoItem)ToDo;
                _navigationService.NavigateTo<ToDoVM>(item);
            });
        }

        public void RecieveMessage(CreatedToDoMessage message)
        {
            ToDoItem toDoItem = message.Data as ToDoItem;
            Items.Add(toDoItem);
        }

        public ToDoListVM(IMessenger messenger, IMyNavigationService navigationService)
        {
            _messenger = messenger;
            _navigationService = navigationService;
            _messenger.Register<CreatedToDoMessage>(this, RecieveMessage);
        }
    }
}
