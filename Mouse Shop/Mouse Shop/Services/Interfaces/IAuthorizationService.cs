using Mouse_Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Interfaces
{
    interface IAuthorizationService
    {
        public void Authorize(User user);
        public void Register(User user, string confirm);
        public bool CheckExists(User user);
        public User GetUser(string mail, string password);
        public bool CheckInputs(User user, string confirm);
    }
}
