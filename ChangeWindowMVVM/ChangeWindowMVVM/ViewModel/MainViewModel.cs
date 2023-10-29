using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeWindowMVVM.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                Set(ref _currentViewModel, value);
            }
        }
        public RelayCommand<object> ChangeCommand
        {
            get => new(param =>
            {
                switch (param as string)
                {
                    case "1":
                        CurrentViewModel = App.Container.GetInstance<FirstViewModel>();
                        break;
                    case "2":
                        CurrentViewModel = App.Container.GetInstance<SecondViewModel>();
                        break;
                    case "3":
                        CurrentViewModel = App.Container.GetInstance<ThirdViewModel>();
                        break;
                }
            });
        }
    }
}
