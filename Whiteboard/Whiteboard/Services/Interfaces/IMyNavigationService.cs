using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.Model;

namespace Whiteboard.Services.Interfaces
{
    public interface IMyNavigationService
    {
        public void NavigateTo<T>(object? data = null) where T : ViewModelBase;
        public void NavigateWarning<T>(bool warning) where T : ViewModelBase;
    }
}
