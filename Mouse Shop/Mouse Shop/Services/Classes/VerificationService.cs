using Mouse_Shop.Model;
using Mouse_Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Classes
{
    internal class VerificationService : IVerificationService
    {
        private readonly ISerializeService _serializeService;
        public List<User> Users { get; set; }
        public VerificationService(ISerializeService serializeService)
        {
            _serializeService = serializeService;
        }
        public User IsMatch(string mail, string code)
        {
            using FileStream fs = new("users.json", FileMode.OpenOrCreate);
            using StreamReader sr = new StreamReader(fs);
            Users = _serializeService.Deserialize<List<User>>(sr.ReadToEnd());
            User res = Users.Find(x => x.Mail == mail && x.VerifyCode.ToString() == code);
            return res;
        }
    }
}
