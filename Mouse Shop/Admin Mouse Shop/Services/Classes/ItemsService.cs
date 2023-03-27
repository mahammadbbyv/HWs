using Admin_Mouse_Shop.Model;
using Admin_Mouse_Shop.Services.Interfaces;
using Admin_Mouse_Shop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Mouse_Shop.Services.Classes
{
    class ItemsService : IItemsService, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        private readonly IMyNavigationService _myNavigationService;
        public ItemsService(IMyNavigationService myNavigationService)
        {
            _myNavigationService = myNavigationService;
        }

        protected void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }
        public void Add(Mouse item)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            if (Find(item) == -1)
            {
                if (item == null || item.Company == null || item.Model == null || item.Price == 0 || item.DPI == 0 || item.ImagePath == null)
                {
                    MessageBox.Show("Fill in all of the attributes!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    window.Products.Add(item);
                }
            }
            else
            {
                MessageBox.Show("This product already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Set(Mouse prevItem, Mouse newItem)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            if (newItem == null || newItem.Company == null || newItem.Model == null || newItem.Price == 0 || newItem.DPI == 0 || newItem.ImagePath == null)
            {
                MessageBox.Show("Fill in all of the attributes!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                window.Products[Find(prevItem)] = newItem;
            }
        }

        public int Find(Mouse item)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            for (int i = 0; i < window.Products.Count; i++)
            {
                if (window.Products[i].Model == item.Model && window.Products[i].Company == item.Company)
                {
                    return i;
                }
            }
            return -1;
        }
        public void Edit(string Company, string Model)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            Mouse tmp = new() { Model = Model, Company = Company };
            if (Find(tmp) != -1)
            {
                _myNavigationService.NavigateTo<ChangeViewModel>(window.Products[Find(tmp)]);
                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, window.Products));
            }
            else
            {
                MessageBox.Show("There is no such product!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Delete(string Company, string Model)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            Mouse tmp = new() { Model = Model, Company = Company };
            if (Find(tmp) != -1)
            {
                window.Products.RemoveAt(Find(tmp));
                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, window.Products));
            }
            else
            {
                MessageBox.Show("There is no such product!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
