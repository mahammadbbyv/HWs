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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            string gettext = sender.ToString();
            gettext = gettext[32].ToString();
            textNum.Content += gettext;
            string? val = textNum.Content.ToString();
            if (val.Length < 10)
            {
                if (((val.Length / 2) + 2) * textNum.FontSize > 600)
                {
                    textNum.FontSize *= 0.8;
                }
            }
            else
            {
                textError.Content = "Number should be less than 10 figures!!!";
                string tmp = "";
                string label = textNum.Content.ToString();
                for(int i = 0; i < textNum.Content.ToString().Length - 1; i++) {
                    tmp += label[i];
                }
                textNum.Content = tmp;
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            string gettext = sender.ToString();
            textEq.Content += gettext;
            textNum.Content = string.Empty;
            textError.Content = string.Empty;
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if(textNum.Content.ToString() == "1910")
            {
                Window1 window = new();
                window.Show();
            }
                textEq.Content += textNum.Content.ToString();
                var result = new Interpreter().Eval(textEq.Content.ToString());
                textNum.Content = result.ToString();
                textEq.Content = string.Empty;
                if (result.ToString().Length < 10)
                {
                    if (((result.ToString().Length / 2) + 2) * textNum.FontSize > 600)
                    {
                        textNum.FontSize *= 0.8;
                    }
                }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if(textNum.Content != string.Empty)
            {
                textNum.Content = string.Empty;
            }
            else
            {
                for(int i = 0; i < nums.Count; i++)
                {
                    nums.RemoveAt(i);
                }
                textEq.Content = string.Empty;
                textError.Content = string.Empty;
            }
            textNum.FontSize = 200;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            textNum.Content = string.Empty;
        }
    }
}
