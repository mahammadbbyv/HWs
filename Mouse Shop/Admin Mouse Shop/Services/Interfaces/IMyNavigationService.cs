using GalaSoft.MvvmLight;
using Admin_Mouse_Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.Services.Interfaces
{
    public interface IMyNavigationService
    {
        public void NavigateTo<T>(object? data = null) where T : ViewModelBase;
        public void SendData<T>(object data, bool negative = false) where T : ViewModelBase;
        public void SendChange<T>(object data) where T : ViewModelBase;
    }
}
