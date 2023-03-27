using Mouse_Shop.Model;
using Mouse_Shop.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Mouse_Shop.Services.Classes
{
    internal class PurchaseService : IPurchaseService
    {
        public string APIKey = "rasimbabayev9g19@gmail.com_73f58053a09e5165fba4d6610c01b873ab1dfd15206a3bcbc30bcbbd79bb1ca540f99f82";
        public Dictionary<string, object> pdf { get; set; } = new();
        public Reciept reciept { get; set; } = new();
        public WebClient WebClient { get; set; } = new();
        public string link { get; set; }
        private readonly ISerializeService _serializeService;
        public PurchaseService(ISerializeService serializeService)
        {
            _serializeService = serializeService;
            pdf.Add("templateId", 1);
            pdf.Add("name", "./invoice.pdf");
            pdf.Add("printBackground", true);
            pdf.Add("margins", "40px 20px 20px 20px");
            pdf.Add("paperSize", "Letter");
            pdf.Add("orientation", "Portrait");
            pdf.Add("header", "");
            pdf.Add("async", false);
            pdf.Add("encrypt", false);
            pdf.Add("issuer_company", "Maga's Fuel");
            //pdf.Add("logo", "telegram-bots-maga.cx.ua/logo.png");
            pdf.Add("issuer_name", "Maga");
            WebClient.Headers.Add("x-api-key", APIKey);
        }
        public void GenerateReciept(ObservableCollection<Product> Products, float total)
        {
            foreach (Product product in Products)
            {
                reciept.items.Add(new Item()
                {
                    name = $"{product.Mouse.Company} {product.Mouse.Model}",
                    price = product.Mouse.Price * product.Count
                });
            }
            User user = _serializeService.Deserialize<User>(File.ReadAllText("current_user.json"));
            reciept.client_email = user.Mail;
            reciept.client_name = user.Name;
            reciept.total = total;
            pdf.Add("templateData", JsonSerializer.Serialize(reciept));
            string jsonPayload = JsonSerializer.Serialize(pdf);
            string response = WebClient.UploadString("https://api.pdf.co/v1/pdf/convert/from/html", jsonPayload);
            JObject json = JObject.Parse(response);
            string resultFileUrl = json["url"].ToString();
            int i = 0;
            while (File.Exists($"reciept{i}.pdf"))
            {
                i++;
            }
            WebClient.DownloadFile(resultFileUrl, $"reciept{i}.pdf");
            pdf.Remove("templateData");
        }
        public void SendReciept()
        {
            MessageBox.Show("We've sent you your reciept!");
            MailMessage mail = new();
            SmtpClient SmtpServer = new("smtp.gmail.com");
            mail.From = new MailAddress("rasimbabayev9g19@gmail.com");
            User user = _serializeService.Deserialize<User>(File.ReadAllText("current_user.json"));
            mail.To.Add(user.Mail);
            mail.Subject = "Your reciept";
            mail.Body = $"<h3>{user.Name} {user.Surname}, this is your code for reciept!</h3>";
            mail.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment; 
            int i = 0;
            while (File.Exists($"reciept{i}.pdf"))
            {
                i++;
            }
            i--;
            attachment = new System.Net.Mail.Attachment($"reciept{i}.pdf");
            mail.Attachments.Add(attachment);

            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new System.Net.NetworkCredential("rasimbabayev9g19@gmail.com", "gitobwidmpsvwsdv");
            SmtpServer.EnableSsl = true;
            SmtpServer.SendMailAsync(mail);
        }
    }
}
