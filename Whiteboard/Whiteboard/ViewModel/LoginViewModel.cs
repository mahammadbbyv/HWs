using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Whiteboard.Services.Interfaces;

namespace Whiteboard.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IMyNavigationService _myNavigationSevice;
        private readonly IUserManageService _userManageService;

        public string Email { get; set; } = string.Empty;
        public bool IsCheckedKey { get; set; } = false;

        public LoginViewModel(IMyNavigationService navigationService, IUserManageService userManageService)
        {
            _myNavigationSevice = navigationService;
            _userManageService = userManageService;
        }

        public RelayCommand<object> LoginCommand
        {
            get => new(
             param =>
             {
                 try
                 {
                     var password = param as PasswordBox;
                     var user = _userManageService.GetUser(Email, password.Password);
                     
                     _userManageService.SetCurrentUser(user);

                     using FileStream fs = new("remember.json", FileMode.Create, FileAccess.Write);
                     using StreamWriter sw = new StreamWriter(fs);

                     sw.Write(JsonSerializer.Serialize<bool>(IsCheckedKey));

                     _myNavigationSevice.NavigateTo<HomeViewModel>();

                     MessageBox.Show($"{user.Email} logged in");
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("User does not exist");
                 }
             });
        }

        public RelayCommand RegisterCommand
        {
            get => new(() =>
            {
                _myNavigationSevice.NavigateTo<RegisterViewModel>();
            });
        }
    }
}
