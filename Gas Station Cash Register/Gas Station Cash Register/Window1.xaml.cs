using SendGrid.Helpers.Mail;
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
using System.Windows.Shapes;

namespace Gas_Station_Cash_Register
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex asd = new("^\\S+@\\S+\\.\\S+$");
            if (asd.IsMatch(email.Text.ToString()))
            {
                MessageBox.Show("Sent", "Nice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Enter a valid email!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           

            MailMessage mail = new();
            SmtpClient SmtpServer = new("smtp.gmail.com");
            mail.From = new MailAddress("magasfuelcompany@fuel.com");
            mail.To.Add(email.Text.ToString());
            mail.Subject = "Invoice:";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("C:\\Users\\Bemga\\OneDrive\\Рабочий стол\\Gas Station Cash Register\\Gas Station Cash Register\\bin\\Debug\\net6.0-windows\\asd.pdf");
            mail.Attachments.Add(attachment);

            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new System.Net.NetworkCredential("rasimbabayev9g19@gmail.com", "your created password");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            Close();
        }
    }
}
