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
using System.Windows;

namespace Admin_Mouse_Shop.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IItemsService _itemsService;
        private readonly IMyNavigationService _myNavigationService;
        public ObservableCollection<Mouse> Products { get; set; } = new();
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
        public void ReceiveMessage(DataMessage message)
        {
            _itemsService.Add(message.Data as Mouse);
        }

        internal void MainClose()
        {

            File.WriteAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json", JsonSerializer.Serialize(Products));
            File.WriteAllText("last_id.txt", App.Container.GetInstance<AddViewModel>().Id.ToString());
        }

        internal void MainOpen()
        {
            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json"))
            {
                Products = JsonSerializer.Deserialize<ObservableCollection<Mouse>>(File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json"));
                App.Container.GetInstance<AddViewModel>().Id = Convert.ToInt32(File.ReadAllText("last_id.txt"));
            }
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

        public RelayCommand<object> EditCommand
        {
            get => new(param =>
            {
                _itemsService.Edit((int)param);
            });
        }

        public RelayCommand<object> DeleteCommand
        {
            get => new(param =>
            {
                _itemsService.Delete((int)param);
            });
        }
        //public RelayCommand AddViewCommand
        //{
        //    get => new(() =>
        //    {
        //        _myNavigationService.NavigateTo<AddViewModel>();
        //    });
        //}
        //public RelayCommand DeleteViewCommand
        //{
        //    get => new(() =>
        //    {
        //        _myNavigationService.NavigateTo<DeleteViewModel>();
        //    });
        //}
        //public RelayCommand EditViewCommand
        //{
        //    get => new(() =>
        //    {
        //        _myNavigationService.NavigateTo<EditViewModel>();
        //    });
        //}
    }
}
