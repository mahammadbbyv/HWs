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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mouse_Shop.ViewModel
{
    internal class VerifyViewModel : ViewModelBase
    {
        public string WrittenCode { get; set; }
        public string WrittenMail { get; set; }
        private readonly IMessenger _messenger;
        private readonly IVerificationService _verificationService;
        private readonly IMyNavigationService _navigationService;
        private readonly IAuthorization _authorization;
        public VerifyViewModel(IMessenger messenger, IVerificationService verificationService, IMyNavigationService navigationService, IAuthorization authorization)
        {
            _messenger = messenger;
            _verificationService = verificationService;
            _navigationService = navigationService;
            _authorization = authorization;
        }
        public RelayCommand VerifyCommand
        {
            get => new(() =>
            {
                bool res = _verificationService.IsMatch(WrittenMail, WrittenCode);

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
