using GalaSoft.MvvmLight;
using Mouse_Shop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Interfaces
{
    internal interface IPurchaseService
    {
        public void GenerateReciept(ObservableCollection<Product> Products, float subtotal);
        public void SendReciept(MailMessage mail);
    }
}
