using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mouse_Shop.Services;
using Mouse_Shop.Services.Interfaces;
using Mouse_Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouse_Shop.Services.Classes;
using System.DirectoryServices;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace Mouse_Shop.ViewModel
{
    class AuthorizationViewModel : ViewModelBase
    {
        private readonly IAuthorization _authorization;
        private readonly IMyNavigationService _navigationService;
        public User user { get; set; } = new() { Mail = "rasimbabayev9g19@gmail.com", Name="Maga", Surname="Babayev", Password="ASDqwe123-" };
        public AuthorizationViewModel(IAuthorization authorization, IMyNavigationService navigationService)
        {
            _authorization = authorization;
            _navigationService = navigationService;
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
                    user.Password = password.Password.ToString();
                    _authorization.Register(user, confirm.Password.ToString());
                }
            });
        }
        public RelayCommand<object> LoginCommand
        {
            get => new(
            param =>
            {
                try
                {
                    var password = param as PasswordBox;

                    var res = _authorization.GetUser(user.Mail, user.Password);

                    MessageBox.Show($"{res.Name} logged in");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("User doesn't exist");
                }
            });
        }
        public RelayCommand VerifyCommand
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<VerifyViewModel>();
            });
        }
    }
}
