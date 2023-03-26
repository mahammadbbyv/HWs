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

namespace Mouse_Shop.ViewModel
{
    class ProductsViewModel : ViewModelBase
    {
        IMyNavigationService _navigationService;
        public ObservableCollection<Mouse> Products { get; set; } = new()
        {
            new Mouse()
            {
                ImagePath = "https://strgimgr.umico.az/sized/840/338545-732dc8904734160a1fce2f2fc8813842.jpg",
                Model = "Viper",
                Company = "Razer",
                DPI = 20000,
                Wireless = true,
                Gaming = true,
                Price = 69.99f
            },
            new Mouse()
            {
                ImagePath = "https://bytelecom.az/media/2022/04/15/product_images/3838/41MexvlK3YL._AC_SL1000_.jpg",
                Model = "G305",
                Company = "Logitech",
                DPI = 16000,
                Wireless = true,
                Gaming = true,
                Price = 34.99f
            },
            new Mouse()
            {
                ImagePath = "https://bytelecom.az/media/2022/04/15/product_images/3838/41MexvlK3YL._AC_SL1000_.jpg",
                Model = "Lol",
                Company = "gg",
                DPI = 16000,
                Wireless = true,
                Gaming = true,
                Price = 34.99f
            },
            new Mouse()
            {
                ImagePath = "https://bytelecom.az/media/2022/04/15/product_images/3838/41MexvlK3YL._AC_SL1000_.jpg",
                Model = "qwe",
                Company = "asd",
                DPI = 16000,
                Wireless = true,
                Gaming = true,
                Price = 34.99f
            }
        };
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
        public ProductsViewModel(IMyNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
