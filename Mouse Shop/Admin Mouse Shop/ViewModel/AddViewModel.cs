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
using System.Windows;

namespace Admin_Mouse_Shop.ViewModel
{
    class AddViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private static int _id = 0;
        public int Id {
            get { return _id; }
            set { _id = value; } }
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
                Product.Id = Id;
                Id++;
                _navigationService.SendData<MainViewModel>(Product);
                Product = new Mouse();
            });
        }
        public RelayCommand<object> CategoryCommand
        {
            get => new(param =>
            {
                switch(param as string)
                {
                    case "Wireless Gaming":
                        Product.Wireless = true;
                        Product.Gaming = true;
                        break;
                    case "Wired Gaming":
                        Product.Wireless = false;
                        Product.Gaming = true;
                        break;
                    case "Wireless Office":
                        Product.Wireless = true;
                        Product.Gaming = false;
                        break;
                    case "Wired Office":
                        Product.Wireless = false;
                        Product.Gaming = false;
                        break;
                }
            });
        }
    }
}
