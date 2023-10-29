using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Interfaces
{
    internal interface IServerService
    {
        public void FtpUploadString(string text, string file);
        public string FtpDownloadString(string file);
    }
}
