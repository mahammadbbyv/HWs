using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Model
{
    internal class Reciept
    {
        public bool paid { get; set; } = true;
        public string invoice_id { get; set; }
        public DateTime invoice_date { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime invoice_dateDue { get; set; } = DateTime.Now.ToLocalTime();
        public string issuer_name { get; set; } = "Maga";
        public string issuer_company { get; set; } = "Maga's Fuel";
        public string issuer_address { get; set; } = "Necefqulu Refiyev kuechesi, 14";
        public string issuer_website { get; set; } = "telegram-bots-maga.cx.ua";
        public string issuer_email { get; set; } = "rasimbabayev9g19@gmail.com@gmail.com";
        public string client_name { get; set; }
        public string client_company { get; set; } = "";
        public string client_address { get; set; } = "";
        public string client_email { get; set; }
        public List<Item> items { get; set; } = new();
        public int discount { get; set; } = 0;
        public int tax { get; set; } = 0;
        public float total { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public float price { get; set; }
    }
}
