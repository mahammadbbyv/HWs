using Admin_Mouse_Shop.Model;
using Admin_Mouse_Shop.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.ViewModel
{
    class AddViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IMyNavigationService _navigationService;
        public AddViewModel(IMyNavigationService navigationService, IItemsService itemsService)
        {
            _navigationService = navigationService;
        }
        private Mouse _product = new();
        public Mouse Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChange(nameof(Product));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RelayCommand ImagePickCommand
        {
            get => new(() =>
            {
                OpenFileDialog dialog = new();
                dialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.jfif";

                var result = dialog.ShowDialog();
                if (result == true)
                {
                    Product.ImagePath = (string)dialog.FileName;
                }
            });
        }
        public RelayCommand AddCommand
        {
            get => new(() =>
            {
                _navigationService.SendData<MainViewModel>(Product);
                Product = new Mouse();
            });
        }
        public RelayCommand WirelessGamingCommand
        {
            get => new(() =>
            {
                Product.Wireless = true;
                Product.Gaming = true;
            });
        }
        public RelayCommand WiredGamingCommand
        {
            get => new(() =>
            {
                Product.Wireless = false;
                Product.Gaming = true;
            });
        }
        public RelayCommand WirelessOfficeCommand
        {
            get => new(() =>
            {
                Product.Wireless = true;
                Product.Gaming = false;
            });
        }
        public RelayCommand WiredOfficeCommand
        {
            get => new(() =>
            {
                Product.Wireless = false;
                Product.Gaming = false;
            });
        }
    }
}
