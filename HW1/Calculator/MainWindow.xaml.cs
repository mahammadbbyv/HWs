using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DynamicExpresso;


namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<int> nums { get; set; } = new();
        public string operations { get; set; } = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            string gettext = sender.ToString();
            gettext = gettext[32].ToString();
            text.Content += gettext;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            string gettext = sender.ToString();
            gettext = gettext[32].ToString();
            int num = Convert.ToInt32(text.Content);
            nums.Add(num);
            text2.Content += num.ToString();
            text2.Content += gettext;
            operations += gettext;
            //string tmp = text2.Content.ToString();
            //MessageBox.Show($"{tmp.Length}", "asd");
            text.Content = "";
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            text2.Content += text.Content.ToString();
            text.Content = string.Empty;
            var result = new Interpreter().Eval(text2.Content.ToString());
            text.Content = result.ToString();
            text2.Content = string.Empty;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if(text.Content != string.Empty)
            {
                text.Content = string.Empty;
            }
            else
            {
                for(int i = 0; i < nums.Count; i++)
                {
                    nums.RemoveAt(i);
                }
                text2.Content = string.Empty;
            }
        }
    }
}
