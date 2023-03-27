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

namespace Mouse_Shop.ViewModel
{
    class ProductsViewModel : ViewModelBase
    {
        IMyNavigationService _navigationService;
        private readonly ISerializeService _serializeService;

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
        public ProductsViewModel(IMyNavigationService navigationService, ISerializeService serializeService)
        {
            _navigationService = navigationService;
            _serializeService = serializeService;
        }

        internal void MainOpen()
        {
            Products = _serializeService.Deserialize<ObservableCollection<Mouse>>(File.ReadAllText(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json"));
        }
    }
}
