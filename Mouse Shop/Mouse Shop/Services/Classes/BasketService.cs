using Mouse_Shop.Model;
using Mouse_Shop.Services.Interfaces;
using Mouse_Shop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Classes
{
    internal class BasketService : IBasketService, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        private readonly IMyNavigationService _navigationService;
        public BasketService(IMyNavigationService navigationService) => _navigationService = navigationService;
        protected void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }
        public ObservableCollection<Product> Addition(ObservableCollection<Product> Products, Product product)
        {
            if(Exists(Products, product) != -1)
            {
                int tmp = Exists(Products, product);
                Products[tmp].Count++;
            }
            else
            {
                Products.Add(product);
            }
            _navigationService.SendData<StoreViewModel>(Products[Exists(Products, product)].Mouse.Price);
            return Products;
        }
        public int Exists(ObservableCollection<Product> Products, Product product)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Mouse == product.Mouse)
                {
                    return i;
                }
            }
            return -1;
        }
        public ObservableCollection<Product> Substraction(ObservableCollection<Product> Products, Product product)
        {
            if(Products[Exists(Products, product)].Count > 1)
            {
                _navigationService.SendData<StoreViewModel>(Products[Exists(Products, product)].Mouse.Price, true);
                Products[Exists(Products, product)].Count--;
            }
            else
            {
                _navigationService.SendData<StoreViewModel>(Products[Exists(Products, product)].Mouse.Price, true);
                Products.RemoveAt(Exists(Products, product));
                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Products));
            }
            return Products;
        }
    }
}
