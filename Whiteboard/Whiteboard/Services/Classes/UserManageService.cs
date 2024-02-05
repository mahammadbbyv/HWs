using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Whiteboard.Context;
using Whiteboard.Model;
using Whiteboard.Services.Interfaces;

namespace Whiteboard.Services.Classes
{
    public class UserManageService : IUserManageService
    {
        public UserModel CurrentUser { get; set; }

        private readonly ISerializeService _serializeService;
        private readonly IMyNavigationService _navigationService;
        private readonly IMessenger _messenger;

        private readonly WhiteboardDbContext _context;

        public UserManageService(ISerializeService serializeService, IMyNavigationService navigationService, IMessenger messenger, WhiteboardDbContext context)
        {
            _serializeService = serializeService;
            _navigationService = navigationService;
            _messenger = messenger;
            _context = context;
        }

        public void Authorize(UserModel user)
        {
            var result = _context.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
        }

        public void Register(UserModel user, string confirm)
        {
            if (!CheckExists(user) && CheckInputs(user, confirm))
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                
                SetCurrentUser(user);
            }
            else if (CheckExists(user))
            {
                MessageBox.Show("This mail is already used!");
            }
        }

        public bool CheckExists(UserModel user)
        {
            return _context.Users.Any(x => x.Email == user.Email);
        }

        public UserModel GetUser(string mail, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Email == mail);
        }

        public bool CheckInputs(UserModel user, string confirm)
        {
            if (Regex.IsMatch(user.Email, "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
            {
                if (Regex.IsMatch(user.Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}"))
                {
                    if (user.Username.Length > 3)
                    {

                        if (user.Password == confirm)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Passwords don't match!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Userame is too short!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Password doesn't match to our conditions!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("E-Mail doesn't match to our conditions!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return false;
        }

        public void SetCurrentUser(UserModel user)
        {
            CurrentUser = user;
        }

        public UserModel GetCurrentUser()
        {
            return CurrentUser;
        }
    }
}
