using Admin_Mouse_Shop.Messages;
using Admin_Mouse_Shop.Model;
using Admin_Mouse_Shop.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.ViewModel
{
    internal class MainViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private readonly IMessenger _messenger;
        private readonly IItemsService _itemsService;
        private readonly IMyNavigationService _myNavigationService;
        public ObservableCollection<Mouse> Products { get; set; } = new();
        private ViewModelBase _currentViewModel;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

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
        public void ReceiveMessage(DataMessage message)
        {
            _itemsService.Add(message.Data as Mouse);
            OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Products));
        }

        internal void MainClose()
        {
            File.WriteAllText(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json", JsonSerializer.Serialize(Products));
        }

        internal void MainOpen()
        {
            if(File.Exists(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json"))
                Products = JsonSerializer.Deserialize<ObservableCollection<Mouse>>(File.ReadAllText(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json"));
        }

        public MainViewModel(IMessenger messenger, IItemsService itemsService, IMyNavigationService myNavigationService)
        {
            CurrentViewModel = App.Container.GetInstance<AddViewModel>();
            _itemsService = itemsService;

            _messenger = messenger;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);
            _messenger.Register<DataMessage>(this, ReceiveMessage);
            _myNavigationService = myNavigationService;
        }
        public RelayCommand AddViewCommand
        {
            get => new(() =>
            {
                _myNavigationService.NavigateTo<AddViewModel>();
            });
        }
        public RelayCommand DeleteViewCommand
        {
            get => new(() =>
            {
                _myNavigationService.NavigateTo<DeleteViewModel>();
            });
        }
        public RelayCommand EditViewCommand
        {
            get => new(() =>
            {
                _myNavigationService.NavigateTo<EditViewModel>();
            });
        }
    }
}
