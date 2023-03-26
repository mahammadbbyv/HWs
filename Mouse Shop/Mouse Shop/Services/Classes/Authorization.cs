using GalaSoft.MvvmLight.Messaging;
using Mouse_Shop.Messages;
using Mouse_Shop.Model;
using Mouse_Shop.Services.Interfaces;
using Mouse_Shop.View;
using Mouse_Shop.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;

namespace Mouse_Shop.Services.Classes
{
    class Authorization : IAuthorization
    {
        public List<User> Users { get; set; } = new();
        private readonly ISerializeService _serializeService;
        private readonly IMyNavigationService _navigationService;
        private readonly IMessenger _messenger = new Messenger();
        public Authorization(ISerializeService serializeService, IMyNavigationService navigationService, IMessenger messenger)
        {
            _serializeService = serializeService;
            _navigationService = navigationService;
            _messenger = messenger;
        }

        public void Authorize(User user)
        {
            using FileStream fs = new("users.json", FileMode.OpenOrCreate);
            using StreamReader sr = new(fs);
            if (sr.ReadToEnd() != string.Empty)
            {
                fs.Position = 0;
                Users = _serializeService.Deserialize<List<User>>(sr.ReadToEnd());
            }
            sr.Close();
            fs.Close();
            if (CheckExists(user)) { 
                var result = Users.Find(x => x.Mail == user.Mail && x.Password == user.Password);
                if (result != null && result.Confirmed && user.Password == result.Password)
                {
                    _navigationService.NavigateTo<StoreViewModel>();
                    File.WriteAllText("current_user.json", _serializeService.Serialize<User>(result));
                }
                else { MessageBox.Show("Password is incorrect!"); }
            }
            else
            {
                MessageBox.Show("There is no such email!");
            }

        }

        public void Register(User user, string confirm)
        {
            if (!CheckExists(user) && CheckInputs(user, confirm))
            {
                using FileStream fs = new("users.json", FileMode.OpenOrCreate, FileAccess.Write);
                using StreamWriter sw = new(fs);
                MessageBox.Show("We've sent you an email verification!");
                MailMessage mail = new();
                SmtpClient SmtpServer = new("smtp.gmail.com");
                mail.From = new MailAddress("magasfuelcompany@fuel.com");
                mail.To.Add(user.Mail);
                mail.Subject = "Verify yourself";
                Random rand = new();
                int code = rand.Next(100000, 999999);
                mail.Body = $"<p>{user.Name} {user.Surname}, this <b>{code}</b> is your code for verification.</p>";
                mail.IsBodyHtml = true;

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("rasimbabayev9g19@gmail.com", "gitobwidmpsvwsdv");
                SmtpServer.EnableSsl = true;
                SmtpServer.SendMailAsync(mail);
                user.VerifyCode = code;
                Users.Add(user);
                sw.Write(JsonSerializer.Serialize(Users));
            }
            else if (CheckExists(user))
            {
                MessageBox.Show("This mail is already used!");
            }

        }

        public bool CheckExists(User user)
        {
            using FileStream fs = new("users.json", FileMode.OpenOrCreate, FileAccess.Read);
            using StreamReader sr = new(fs);
            if (sr.ReadToEnd() != string.Empty)
            {
                fs.Position = 0;
                Users = _serializeService.Deserialize<List<User>>(sr.ReadToEnd());
            }
            User res = Users.Find(x => x.Mail == user.Mail);
            return res != null;
        }
        public User GetUser(string mail, string password)
        {
            using FileStream fs = new("users.json", FileMode.OpenOrCreate, FileAccess.Read);
            using StreamReader sr = new(fs);
            if (mail != null && password != null)
            {
                fs.Position = 0;
                if (sr.ReadToEnd() != string.Empty)
                {
                    Users = _serializeService.Deserialize<List<User>>(sr.ReadToEnd());
                }
                User res = Users.Find(x => x.Mail == mail && x.Password == password);
                if (!res.Confirmed)
                {
                    MessageBox.Show("Verify your account!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _navigationService.NavigateTo<VerifyViewModel>();
                    return new User();
                }
                else
                {
                    return res;
                }
            }
            else
            {
                return new User();
            }
        }

        public bool CheckInputs(User user, string confirm)
        {
            if (Regex.IsMatch(user.Mail, "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
            {
                if (Regex.IsMatch(user.Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}"))
                {
                    if (user.Name.Length > 3)
                    {
                        if (user.Surname.Length > 3)
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
                            MessageBox.Show("Surname is too short!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Name is too short!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
