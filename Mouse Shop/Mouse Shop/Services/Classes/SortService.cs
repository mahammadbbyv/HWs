using Mouse_Shop.Model;
using Mouse_Shop.Services.Interfaces;
using Mouse_Shop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Classes
{
    internal class SortService : ISortService, INotifyCollectionChanged
    {
        public ObservableCollection<Mouse> products = new();
        private readonly ISerializeService _serializeService;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        protected void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        public SortService(ISerializeService serializeService)
        {
            _serializeService = serializeService;
        }

        public void SortByCategory(string Case)
        {
            if(File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json"))
                products = _serializeService.Deserialize<ObservableCollection<Mouse>>(File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()).ToString() + "\\products.json"));
            var window = App.Container.GetInstance<ProductsViewModel>();
            switch (Case)
            {
                case "All":
                    window.Products.Clear();
                    for (int i = 0; i < products.Count; i++)
                    {
                        window.Products.Add(products[i]);
                    }
                    break;
                case "WirelessOffice":
                    window.Products.Clear();
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].Wireless && !products[i].Gaming)
                        {
                            window.Products.Add(products[i]);
                        }
                    }
                    break;
                case "WirelessGaming":
                    window.Products.Clear();
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].Wireless && products[i].Gaming)
                        {
                            window.Products.Add(products[i]);
                        }
                    }
                    break;
                case "WiredOffice":
                    window.Products.Clear();
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (!products[i].Wireless && !products[i].Gaming)
                        {
                            window.Products.Add(products[i]);
                        }
                    }
                    break;
                case "WiredGaming":
                    window.Products.Clear();
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (!products[i].Wireless && products[i].Gaming)
                        {
                            window.Products.Add(products[i]);
                        }
                    }
                    break;
            }
        }
    }
}
