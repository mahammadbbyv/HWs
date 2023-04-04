using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                Set(ref _currentViewModel, value);
            }
        }

        public void ReceiveMessage(ViewModelBase message)
        {
            CurrentViewModel = message;
        }

        public MainViewModel(IMessenger messenger)
        {
            CurrentViewModel = App.Container.GetInstance<ListViewModel>();

            _messenger = messenger;
            _messenger.Register<ViewModelBase>(this, ReceiveMessage);
        }
    }
}
