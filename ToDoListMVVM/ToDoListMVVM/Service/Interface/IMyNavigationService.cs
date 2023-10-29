using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVVM.Service.Interface
{
    interface IMyNavigationService
    {
        public void NavigateTo<T>(object? data = null, bool isNew = false) where T : ViewModelBase;
    }
}
