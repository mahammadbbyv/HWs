using Mouse_Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Classes
{
    internal class ServerService : IServerService
    {
        public void FtpUploadString(string text, string file)

        {
            using WebClient client = new();
            client.DownloadString($"https://www.telegram-bots-maga.cx.ua/{File.ReadAllText("file.txt")}?text={text}&file={file}");
        }
        
        public string FtpDownloadString(string file)
        {

            using WebClient wc = new();
            return wc.DownloadString("https://www.telegram-bots-maga.cx.ua/" + file);
        }
    }
}
