using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Whiteboard.Model;
using Whiteboard.Services.Interfaces;

namespace Whiteboard.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        public UserModel User { get; set; } = new();
        public List<string> Operators { get; set; } = new()
        {
            "050",
            "070",
            "055",
            "077",
            "099",
            "010"
        };

        private readonly IMyNavigationService _navigationService;
        private readonly IUserManageService _userService;

        public RegisterViewModel(IMyNavigationService navigationService, IUserManageService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
        }

        public RelayCommand<object> RegisterCommand
        {
            get => new(
            param =>
            {
                if (param != null)
                {
                    object[] res = (object[])param;

                    var password = (PasswordBox)res[0];
                    var confirm = (PasswordBox)res[1];

                    User.Password = password.Password.ToString();
                    _userService.Register(User, confirm.Password.ToString());

                    _navigationService.NavigateTo<HomeViewModel>();
                }
            });
        }
    }
}
