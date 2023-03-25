using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services
{
    public interface IMyNavigationService
    {
        public void NavigateTo<T>(object? data = null) where T : ViewModelBase;
        public void SendData<T>(object data) where T : ViewModelBase;
    }
}
