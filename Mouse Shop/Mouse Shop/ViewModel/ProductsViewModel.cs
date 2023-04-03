using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouse_Shop.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Mouse_Shop.Services;
using System.Windows;
using Mouse_Shop.Messages;
using System.IO;
using Mouse_Shop.Services.Interfaces;
using Mouse_Shop.Services.Classes;

namespace Mouse_Shop.ViewModel
{
    class ProductsViewModel : ViewModelBase
    {
        private readonly IMyNavigationService _navigationService;
        private readonly ISerializeService _serializeService;
        private readonly IServerService _serverService;
        public ProductsViewModel(IMyNavigationService navigationService, ISerializeService serializeService, IServerService serverService)
        {
            _navigationService = navigationService;
            _serializeService = serializeService;
            _serverService = serverService;
        }

        public ObservableCollection<Mouse> Products { get; set; } = new();
        public RelayCommand<object> AddCommand
        {
            get => new(param =>
            {
                for (int i = 0;  i < Products.Count; i++)
                {
                    if (Products[i].Model == param as string)
                    {
                        _navigationService.SendProduct<CartViewModel>( new Product() { Mouse = Products[i], Count = 1 });
                    }
                }
            });
        }

        public void MainOpen()
        {
            Products = _serializeService.Deserialize<ObservableCollection<Mouse>>(_serverService.FtpDownloadString("products.json"));
        }

        internal void MainClose()
        {
            int i = 0;
            while (File.Exists($"reciept{i}.pdf"))
            {
                File.Delete($"reciept{i}.pdf");
                i++;
            }
        }
    }
}
