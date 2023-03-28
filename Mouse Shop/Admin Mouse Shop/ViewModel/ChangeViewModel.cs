using Admin_Mouse_Shop.Messages;
using Admin_Mouse_Shop.Model;
using Admin_Mouse_Shop.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.ViewModel
{
    class ChangeViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private Mouse _newProduct { get; set; } = new();
        private Mouse _prevProduct { get; set; } = new(); 
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Mouse NewProduct { get { return _newProduct; } set { _newProduct = value; OnPropertyChange(nameof(NewProduct)); } }
        public Mouse PrevProduct { get { return _prevProduct; } set { _prevProduct = value; OnPropertyChange(nameof(PrevProduct)); } }
        private readonly IMessenger _messenger;
        private readonly IItemsService _itemsService;
        private readonly IMyNavigationService _navigationService;

        private void ReceiveMessage(ChangeMessage message)
        {
            Mouse tmp = (Mouse)message.Data;
            PrevProduct = (Mouse)tmp.Clone();
            NewProduct = (Mouse)tmp.Clone();
        }

        public ChangeViewModel(IMessenger messenger, IItemsService itemsService, IMyNavigationService navigationService)
        { 
            _messenger = messenger;
            _itemsService = itemsService;
            _navigationService = navigationService;
            _messenger.Register<ChangeMessage>(this, ReceiveMessage);
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
                    NewProduct.ImagePath = dialog.FileName;
                }
            });
        }
        public RelayCommand SetCommand
        {
            get => new(() =>
            {
                _itemsService.Set(PrevProduct, NewProduct);
                NewProduct = new Mouse();
                _navigationService.NavigateTo<AddViewModel>();
            });
        }
        public RelayCommand<object> CategoryCommand
        {
            get => new(param =>
            {
                switch (param as string)
                {
                    case "Wireless Gaming":
                        NewProduct.Wireless = true;
                        NewProduct.Gaming = true;
                        break;
                    case "Wired Gaming":
                        NewProduct.Wireless = false;
                        NewProduct.Gaming = true;
                        break;
                    case "Wireless Office":
                        NewProduct.Wireless = true;
                        NewProduct.Gaming = false;
                        break;
                    case "Wired Office":
                        NewProduct.Wireless = false;
                        NewProduct.Gaming = false;
                        break;
                }
            });
        }
    }
}
