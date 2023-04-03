using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Mouse_Shop.Messages;
using Mouse_Shop.Model;
using Mouse_Shop.Services;
using Mouse_Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Mouse_Shop.ViewModel
{
    class VerifyViewModel : ViewModelBase
    {
        public List<User> Users { get; set; }
        public string WrittenCode { get; set; }
        public string WrittenMail { get; set; }
        private readonly IVerificationService _verificationService;
        private readonly IMyNavigationService _navigationService;
        private readonly ISerializeService _serializeService;
        private readonly IServerService _serverService;
        public VerifyViewModel(IVerificationService verificationService, IMyNavigationService navigationService, ISerializeService serializeService, IServerService serverService)
        {
            _verificationService = verificationService;
            _navigationService = navigationService;
            _serializeService = serializeService;
            _serverService = serverService;
        }
        public RelayCommand VerifyCommand
        {
            get => new(() =>
            {
                User res = _verificationService.IsMatch(WrittenMail, WrittenCode);
                if (res != null)
                {
                    if (!res.Confirmed) { Users = _serializeService.Deserialize<List<User>>(_serverService.FtpDownloadString("users.json")); int index = Users.FindIndex(x => x.Mail == res.Mail && x.VerifyCode == res.VerifyCode); Users[index].Confirmed = true; MessageBox.Show("You have successfully confirmed your identity!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information); _serverService.FtpUploadString(_serializeService.Serialize<List<User>>(Users), "users.json"); }
                    else{ MessageBox.Show("Your identity was already confirmed!"); }
                }
                else
                {
                    MessageBox.Show("This code is not valid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public RelayCommand BackCommand
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<AuthorizationViewModel>();
            });
        }
    }
}
