using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Model;

namespace ToDoList.ViewModel
{
    class ListViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        
        public ListViewModel(IMessenger messenger)
        {
            _messenger = messenger;
        }
        private ObservableCollection<ToDos> _items = new();
        public ObservableCollection<ToDos> Items
        {
            get => _items;
            set
            {
                Set(ref _items, value);
            }
        }
        public RelayCommand<object> AddCommand
        {
            get => new(param =>
            {
                var tmp = param as TextBox;

                if (tmp != null && tmp.Text != string.Empty)
                    Items.Add(new ToDos { Name = tmp.Text });
            });
        }
        public RelayCommand<object> NavigateCommand
        {
            get => new(param =>
            {
                _messenger.Send<ToDos>(new ToDos { Name = param as string });
                _messenger.Send<ViewModelBase>(App.Container.GetInstance<ItemViewModel>());
            });
        }
    }
}
