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
    class ItemsService : IItemsService
    {
        private readonly IMyNavigationService _myNavigationService;
        public ItemsService(IMyNavigationService myNavigationService)
        {
            _myNavigationService = myNavigationService;
        }

        public void Add(Mouse item)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            if (Find(item.Id) == -1)
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
                window.Products[Find(prevItem.Id)] = newItem;
            }
        }

        public int Find(int Id)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            for (int i = 0; i < window.Products.Count; i++)
            {
                if (window.Products[i].Id == Id)
                {
                    return i;
                }
            }
            return -1;
        }
        public void Edit(int Id)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            _myNavigationService.SendChange<ChangeViewModel>(window.Products[Find(Id)]);
            _myNavigationService.NavigateTo<ChangeViewModel>();
        }

        public void Delete(int Id)
        {
            var window = App.Container.GetInstance<MainViewModel>();
            var res = MessageBox.Show($"Are you sure you want to delete {window.Products[Find(Id)].Company} {window.Products[Find(Id)].Model}?\nThis is irreversible!", "Accetpt?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes) { 
                window.Products.RemoveAt(Find(Id));
                _myNavigationService.NavigateTo<AddViewModel>();
            }
        }

    }
}
