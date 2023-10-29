using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ToDoListMVVM.Message;
using ToDoListMVVM.Model;
using ToDoListMVVM.Service.Interface;

namespace ToDoListMVVM.ViewModel
{
    class AddVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMyNavigationService _navigationService;
        public ToDoItem ToDoItem { get; set; } = new();
        public AddVM(IMessenger messenger, IMyNavigationService navigationService)
        {
            _messenger = messenger;
            _navigationService = navigationService;
        }

        public RelayCommand AddCommand
        {
            get => new(() =>
            {
                ToDoItem.AddeddDate = DateTime.Now;
                _navigationService.NavigateTo<ToDoListVM>(ToDoItem, true);
            });
        }
    }
}
