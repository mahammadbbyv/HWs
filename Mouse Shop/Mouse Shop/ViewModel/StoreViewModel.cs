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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mouse_Shop.ViewModel
{
    class StoreViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IMessenger _messenger;
        private readonly IPurchaseService _purchaseService;
        private readonly IMyNavigationService _myNavigationService;
        private readonly ISort _sort;
        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private float _subtotal = 0.0f;
        public float subtotal
        {
            get { return _subtotal; }
            set
            {
                _subtotal = value;
                OnPropertyChange(nameof(subtotal));
            }
        }
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                Set(ref _currentViewModel, value);
                OnPropertyChange(nameof(CurrentViewModel));
            }
        }
        public void ReceiveMessage(DataMessage message)
        {
            if ((bool)message.IsNegative) 
            { 
                if (subtotal - (float)message.Data <= 0.0f) 
                { 
                    subtotal = 0f; 
                } else 
                { 
                    subtotal -= (float)message.Data; 
                } 
            }
            else 
            {
                subtotal += (float)message.Data;
                string tmp = subtotal.ToString();
                StringBuilder res = new StringBuilder();
                int i = 0;
                if (tmp.Contains(".")) 
                {
                    while (tmp[i].ToString() != ".")
                    {
                        res.Append(tmp[i]);
                        i++;
                    }
                    int j = i + 3;
                    while (j - i > 0 && i < tmp.Length) 
                    { 
                        res.Append(tmp[i]);
                        i++; 
                    }
                }
                else
                {
                    while (i < tmp.Length) { res.Append(tmp[i]); i++; }
                }
                subtotal = Convert.ToSingle(res.ToString());
            }
        }
        public StoreViewModel(IMessenger messenger, IMyNavigationService myNavigationService, IPurchaseService purchaseService, ISort sort)
        {
            _messenger = messenger;
            _myNavigationService = myNavigationService;
            _purchaseService = purchaseService;
            _messenger.Register<DataMessage>(this, ReceiveMessage);
            CurrentViewModel = App.Container.GetInstance<ProductsViewModel>();
            _sort = sort;
        }
        public RelayCommand LogOutCommand
        {
            get => new(() =>
            {
                File.Delete("current_user.json");
                _myNavigationService.NavigateTo<AuthorizationViewModel>();
            });
        }
        public RelayCommand BasketCommand
        {
            get => new(() =>
            {
                CurrentViewModel = App.Container.GetInstance<CartViewModel>();
            });
        }
        public RelayCommand AllCommand
        {
            get => new(() =>
            {
                CurrentViewModel = App.Container.GetInstance<ProductsViewModel>();
                _sort.SortByCategory("All");
            });
        }
        public RelayCommand WirelessOfficeCommand
        {
            get => new(() =>
            {
                CurrentViewModel = App.Container.GetInstance<ProductsViewModel>();
                _sort.SortByCategory("WirelessOffice");
            });
        }
        public RelayCommand WirelessGamingCommand
        {
            get => new(() =>
            {
                CurrentViewModel = App.Container.GetInstance<ProductsViewModel>();
                _sort.SortByCategory("WirelessGaming");
            });
        }
        public RelayCommand WiredOfficeCommand
        {
            get => new(() =>
            {
                CurrentViewModel = App.Container.GetInstance<ProductsViewModel>();
                _sort.SortByCategory("WiredOffice");
            });
        }
        public RelayCommand WiredGamingCommand
        {
            get => new(() =>
            {
                CurrentViewModel = App.Container.GetInstance<ProductsViewModel>();
                _sort.SortByCategory("WiredGaming");
            });
        }
        public RelayCommand PurchaseCommand
        {
            get => new(() =>
            {
                var tmp = App.Container.GetInstance<CartViewModel>();
                if (tmp.Products.Count == 0) { MessageBox.Show("Cart is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                else 
                { 
                    _purchaseService.GenerateReciept(tmp.Products, subtotal);
                    _purchaseService.SendReciept();
                }
            });
        }
    }
}
