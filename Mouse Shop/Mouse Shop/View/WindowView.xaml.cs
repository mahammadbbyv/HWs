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
using System.Windows.Shapes;

namespace Mouse_Shop.View
{
    /// <summary>
    /// Interaction logic for WindowView.xaml
    /// </summary>
    public partial class WindowView : Window
    {
        public WindowView()
        {
            InitializeComponent();

        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        static bool check = false;
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                this.WindowState = WindowState.Normal;
                check = false;
                //maximizeIMG.Source = new BitmapImage(new Uri(@"\Assets\maximize_icon.png", UriKind.Relative));
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                check = true;
                //maximizeIMG.Source = new BitmapImage(new Uri(@"\Assets\minimize_icon.png", UriKind.Relative));
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
