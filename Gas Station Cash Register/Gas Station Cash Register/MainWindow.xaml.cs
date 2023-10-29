using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentEmail.Core;
using System.Text.Json;
using System.Net;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Gas_Station_Cash_Register
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string APIKey = "rasimbabayev9g19@gmail.com_73f58053a09e5165fba4d6610c01b873ab1dfd15206a3bcbc30bcbbd79bb1ca540f99f82";

        public Dictionary<string, object> pdf { get; set; } = new();
        public Invoice invoice { get; set; } = new();
        public WebClient WebClient { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
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
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void LitersButton_Click(object sender, RoutedEventArgs e)
        {
            azn.IsReadOnly = true;
            liters.IsReadOnly = false;
        }

        private void AznButton_Click(object sender, RoutedEventArgs e)
        {
            liters.IsReadOnly = true;
            azn.IsReadOnly = false;
        }

        private void Liters_TextChanged(object sender, TextChangedEventArgs e)
        {
            FuelCostLabel.Content = (Convert.ToDouble((liters.Text.ToString() == "" ? "0.0" : liters.Text.ToString())) * Convert.ToDouble((Price.Text == "" ? "0.0" : Price.Text))).ToString();
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());

        }
        private void Azn_TextChanged(object sender, TextChangedEventArgs e)
        {
            liters.Text = (Convert.ToDouble((azn.Text.ToString() == "" ? "0.0" : azn.Text.ToString())) / Convert.ToDouble((Price.Text == "" ? "0.0" : Price.Text))).ToString();
            FuelCostLabel.Content = azn.Text;
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(Convert.ToDouble((FuelCostLabel.Content.ToString() == "" ? "0.0" : FuelCostLabel.Content.ToString())));
        }

        private async void fuel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = fuel.SelectedItem.ToString()[^2].ToString() + fuel.SelectedItem.ToString()[^1].ToString();
            switch(item)
            {
                case "92":
                    Price.Text = "1.0";
                    break;
                case "95":
                    Price.Text = "2.0";
                    break;
                case "98":
                    Price.Text = "2.3";
                    break;
            }
            //FluentEmail.Core.Models.Attachment attachment = new();
            //attachment.Filename = "logo.png";
            //Email email = new();
            //.From("magasfuel@gmail.com")
            //.To("rasimbabayev9g19@gmail.com")
            //.Body("asd")
            //.SendAsync();
            //email.SetFrom("magasfuel@gmail.com");
            //email.To("rasimbabayev9g19@gmail.com");
            //email.Attach(attachment);
            //email.Send();
        }

        private void Checked_Dog(object sender, RoutedEventArgs e)
        {
            Hot_Dog_Count.IsReadOnly = false;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Hot_Dog_Count.Text.ToString() == "" ? "0.0" : Hot_Dog_Count.Text.ToString())) * Convert.ToDouble(Hot_Dog_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Unchecked_Dog(object sender, RoutedEventArgs e)
        {
            Hot_Dog_Count.IsReadOnly = true;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) - (Convert.ToDouble((Hot_Dog_Count.Text.ToString() == "" ? "0.0" : Hot_Dog_Count.Text.ToString())) * Convert.ToDouble(Hot_Dog_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Checked_Ham(object sender, RoutedEventArgs e)
        {
            Hamburger_Count.IsReadOnly = false;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Hamburger_Count.Text.ToString() == "" ? "0.0" : Hamburger_Count.Text.ToString())) * Convert.ToDouble(Hamburger_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Unchecked_Ham(object sender, RoutedEventArgs e)
        {
            Hamburger_Count.IsReadOnly = true;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) - (Convert.ToDouble((Hamburger_Count.Text.ToString() == "" ? "0.0" : Hamburger_Count.Text.ToString())) * Convert.ToDouble(Hamburger_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Checked_Fry(object sender, RoutedEventArgs e)
        {
            Fries_Count.IsReadOnly = false;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Fries_Count.Text.ToString() == "" ? "0.0" : Fries_Count.Text.ToString())) * Convert.ToDouble(Fries_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Unchecked_Fry(object sender, RoutedEventArgs e)
        {
            Fries_Count.IsReadOnly = true;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) - (Convert.ToDouble((Fries_Count.Text.ToString() == "" ? "0.0" : Fries_Count.Text.ToString())) * Convert.ToDouble(Fries_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Checked_Cola(object sender, RoutedEventArgs e)
        {
            Cola_Count.IsReadOnly = false;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Cola_Count.Text.ToString() == "" ? "0.0" : Cola_Count.Text.ToString())) * Convert.ToDouble(Coca_Cola_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Unchecked_Cola(object sender, RoutedEventArgs e)
        {
            Cola_Count.IsReadOnly = true;
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) - (Convert.ToDouble((Cola_Count.Text.ToString() == "" ? "0.0" : Cola_Count.Text.ToString())) * Convert.ToDouble(Coca_Cola_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Cola_Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Cola_Count.Text.ToString() == "" ? "0.0" : Cola_Count.Text.ToString())) * Convert.ToDouble(Coca_Cola_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Fries_Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Fries_Count.Text.ToString() == "" ? "0.0" : Fries_Count.Text.ToString())) * Convert.ToDouble(Fries_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Hamburger_Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Hamburger_Count.Text.ToString() == "" ? "0.0" : Hamburger_Count.Text.ToString())) * Convert.ToDouble(Hamburger_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void Hot_Dog_Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            FoodCostLabel.Content = (Convert.ToDouble(FoodCostLabel.Content) + (Convert.ToDouble((Hot_Dog_Count.Text.ToString() == "" ? "0.0" : Hot_Dog_Count.Text.ToString())) * Convert.ToDouble(Hot_Dog_Price.Text.ToString())));
            TotalCostLabel.Content = Convert.ToDouble(FoodCostLabel.Content.ToString()) + Convert.ToDouble(FuelCostLabel.Content.ToString());
        }

        private void FoodCostLabel_TextInput(object sender, TextCompositionEventArgs e)
        {
            MessageBox.Show("asd");
        }

        private void Checkout_Button_Click(object sender, RoutedEventArgs e)
        {
            if(FuelCostLabel.Content.ToString() != "0")
            {
                invoice.items.Add(new Item()
                {
                    name = "Fuel " + fuel.SelectedItem.ToString()[^2].ToString() + fuel.SelectedItem.ToString()[^1].ToString(),
                    price = (float)Convert.ToDouble(FuelCostLabel.Content.ToString())
                });
            }
            if (!Hot_Dog_Count.IsReadOnly)
            {
                invoice.items.Add(new Item()
                {
                    name = "Hot Dog",
                    price = (float)Convert.ToDouble(Hot_Dog_Price.Text.ToString()) * Convert.ToInt32(Hot_Dog_Count.Text.ToString())
                });
            }
            if (!Hamburger_Count.IsReadOnly)
            {
                invoice.items.Add(new Item()
                {
                    name = "Hamburger",
                    price = (float)Convert.ToDouble(Hamburger_Price.Text.ToString()) * Convert.ToInt32(Hamburger_Count.Text.ToString())
                });
            }
            if (!Fries_Count.IsReadOnly)
            {
                invoice.items.Add(new Item()
                {
                    name = "Fries",
                    price = (float)Convert.ToDouble(Fries_Price.Text.ToString()) * Convert.ToInt32(Fries_Count.Text.ToString())
                });
            }
            if (!Cola_Count.IsReadOnly)
            {

                invoice.items.Add(new Item()
                {
                    name = "Coca Cola",
                    price = (float)Convert.ToDouble(Coca_Cola_Price.Text.ToString()) * Convert.ToInt32(Cola_Count.Text.ToString())
                });
            }
            invoice.total = Convert.ToInt32(TotalCostLabel.Content.ToString());
            pdf.Add("templateData", JsonSerializer.Serialize(invoice));
            string jsonPayload = JsonSerializer.Serialize(pdf);
            string response = WebClient.UploadString("https://api.pdf.co/v1/pdf/convert/from/html", jsonPayload);
            JObject json = JObject.Parse(response);
            string resultFileUrl = json["url"].ToString();
            WebClient.DownloadFile(resultFileUrl, "C:\\Users\\Bemga\\OneDrive\\Рабочий стол\\Gas Station Cash Register\\Gas Station Cash Register\\bin\\Debug\\net6.0-windows\\asd.pdf");
            Window1 window = new();
            window.ShowDialog();
            Close();
        }
    }
}
