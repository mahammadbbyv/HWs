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
        private readonly IMessenger _messenger;
        private readonly IVerificationService _verificationService;
        private readonly IMyNavigationService _navigationService;
        private readonly IAuthorization _authorization;
        private readonly ISerializeService _serializeService;
        public VerifyViewModel(IMessenger messenger, IVerificationService verificationService, IMyNavigationService navigationService, IAuthorization authorization, ISerializeService serializeService)
        {
            _messenger = messenger;
            _verificationService = verificationService;
            _navigationService = navigationService;
            _authorization = authorization;
            _serializeService = serializeService;
        }
        public RelayCommand VerifyCommand
        {
            get => new(() =>
            {
                User res = _verificationService.IsMatch(WrittenMail, WrittenCode);
                if (res != null)
                {
                    Users = _serializeService.Deserialize<List<User>>(System.IO.File.ReadAllText("users.json"));
                    int index = Users.FindIndex(x => x.Mail == res.Mail && x.VerifyCode == res.VerifyCode);
                    Users[index].Confirmed = true;
                    MessageBox.Show("You have successfully confirmed your identity!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    System.IO.File.WriteAllText("users.json", string.Empty);
                    System.IO.File.WriteAllText("users.json", _serializeService.Serialize<List<User>>(Users));
                }
                else
                {
                    MessageBox.Show("This code ot this mail is not valid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
