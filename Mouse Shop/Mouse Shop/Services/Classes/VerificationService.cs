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
        private readonly IServerService _serverService;
        public List<User> Users { get; set; }
        public VerificationService(ISerializeService serializeService, IServerService serverService)
        {
            _serializeService = serializeService;
            _serverService = serverService;
        }
        public User IsMatch(string mail, string code)
        {
            Users = _serializeService.Deserialize<List<User>>(_serverService.FtpDownloadString("users.json"));
            User res = Users.Find(x => x.Mail == mail && x.VerifyCode.ToString() == code);
            return res;
        }
    }
}
