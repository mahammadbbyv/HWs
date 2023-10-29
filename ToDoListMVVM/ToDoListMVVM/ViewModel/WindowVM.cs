using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ToDoListMVVM.Message;
using ToDoListMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVVM.ViewModel
{
    class WindowVM : ViewModelBase
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
        public void RecieveMessage(NavigationMessage message)
        {
            CurrentViewModel = App.Container.GetInstance(message.Data) as ViewModelBase;
        }

        public WindowVM(IMessenger messenger)
        {
            _messenger = messenger;
            CurrentViewModel = App.Container.GetInstance<ToDoListVM>();
            _messenger.Register<NavigationMessage>(this, RecieveMessage);
        }
    }
}
