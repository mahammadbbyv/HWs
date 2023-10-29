using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.Services.Interfaces
{
    internal interface IServerService
    {
        public void AddImage(string Path);
        public void DeleteImage(string Path);
        public void FtpUploadString(string text, string file);
        public string FtpDownloadString(string file);
    }
}
