using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Mouse_Shop.Messages;
using Mouse_Shop.Model;
using Mouse_Shop.Services;
using Mouse_Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mouse_Shop.ViewModel
{
    class CartViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private readonly IMessenger _messenger;
        IBasketService _basketService;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        protected void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }
        public ObservableCollection<Product> Products { get; set; } = new();
        public void ReceiveMessage(BasketMessage message)
        {
            Products = _basketService.Addition(Products, message.product as Product);
        }
        public CartViewModel(IMessenger messenger, IBasketService basketService)
        {
            _messenger = messenger;
            _basketService = basketService;
            _messenger.Register<BasketMessage>(this, ReceiveMessage);
        }
        public RelayCommand<object> AdditionCommand
        {
            get => new(param =>
            {
                //MessageBox.Show(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "/ShoeImages/MenImages");
                for (int i = 0; i < Products.Count; i++)
                {
                    var tmp = param as Mouse;
                    if (Products[i].Mouse.Model == tmp.Model && Products[i].Mouse.Company == tmp.Company)
                    {
                        Products = _basketService.Addition(Products, Products[i]);
                    }
                }
            });
        }
        public RelayCommand<object> SubtractionCommand
        {
            get => new(param =>
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    var tmp = param as Mouse;
                    if (Products[i].Mouse.Model == tmp.Model && Products[i].Mouse.Company == tmp.Company)
                    {
                        Products = _basketService.Substraction(Products, Products[i]);
                    }
                }
            });
        }
    }
}
