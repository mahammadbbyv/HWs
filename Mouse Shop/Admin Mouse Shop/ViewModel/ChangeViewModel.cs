using Admin_Mouse_Shop.Messages;
using Admin_Mouse_Shop.Model;
using Admin_Mouse_Shop.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.ViewModel
{
    class ChangeViewModel : ViewModelBase
    {
        public Mouse NewProduct { get; set; } = new();
        public Mouse PrevProduct { get; set; } = new();
        private readonly IMessenger _messenger;
        private readonly IItemsService _itemsService;

        private void ReceiveMessage(DataMessage message)
        {
            PrevProduct = (Mouse)message.Data;
            NewProduct = (Mouse)message.Data;
        }

        public ChangeViewModel(IMessenger messenger, IItemsService itemsService)
        { 
            _messenger = messenger;
            _itemsService = itemsService;
            _messenger.Register<DataMessage>(this, ReceiveMessage);
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
                    NewProduct.ImagePath = (string)dialog.FileName;
                }
            });
        }
        public RelayCommand SetCommand
        {
            get => new(() =>
            {
                _itemsService.Set(PrevProduct, NewProduct);
                NewProduct = new Mouse();
            });
        }
        public RelayCommand WirelessGamingCommand
        {
            get => new(() =>
            {
                NewProduct.Wireless = true;
                NewProduct.Gaming = true;
            });
        }
        public RelayCommand WiredGamingCommand
        {
            get => new(() =>
            {
                NewProduct.Wireless = false;
                NewProduct.Gaming = true;
            });
        }
        public RelayCommand WirelessOfficeCommand
        {
            get => new(() =>
            {
                NewProduct.Wireless = true;
                NewProduct.Gaming = false;
            });
        }
        public RelayCommand WiredOfficeCommand
        {
            get => new(() =>
            {
                NewProduct.Wireless = false;
                NewProduct.Gaming = false;
            });
        }
    }
}
