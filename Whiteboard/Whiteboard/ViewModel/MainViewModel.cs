using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Whiteboard.Services.Interfaces;
using Whiteboard.Services.Messages;

namespace Whiteboard.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMyNavigationService _myNavigationSevice;
        private readonly IUserManageService _userManageService;
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                Set(ref _currentViewModel, value);
            }
        }

        public bool IsRemember()
        {
            using FileStream fs = new("remember.json", FileMode.OpenOrCreate, FileAccess.Read);
            using StreamReader sr = new StreamReader(fs);

            if (sr.ReadToEnd() != string.Empty)
            {
                fs.Position = 0;
                return JsonSerializer.Deserialize<bool>(sr.ReadToEnd());
            }
            else
            {
                return false;
            }
        }

        public void ReceiveMessage(NavigationMessage message)
        {
            CurrentViewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
        }

        public MainViewModel(IMessenger messenger, IMyNavigationService myNavigationSevice, IUserManageService userManageService) 
        {
            if (IsRemember())
            {
                CurrentViewModel = App.Container.GetInstance<HomeViewModel>();
            }
            else
            {
                CurrentViewModel = App.Container.GetInstance<LoginViewModel>();
            }

            _messenger = messenger;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);

            _myNavigationSevice = myNavigationSevice;
            _userManageService = userManageService;
        }

        public RelayCommand LogOutCommand
        {
            get => new(() =>
            {
                _userManageService.SetCurrentUser(null);
                _myNavigationSevice.NavigateTo<LoginViewModel>();
            });
        }
    }
}
