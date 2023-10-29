using Mouse_Shop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Interfaces
{
    internal interface IBasketService
    {
        public ObservableCollection<Product> Addition(ObservableCollection<Product> Products, Product product);
        public int Exists(ObservableCollection<Product> Products, Product product);
        public ObservableCollection<Product> Substraction(ObservableCollection<Product> Products, Product product);
    }
}
