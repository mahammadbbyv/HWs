using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
using Maga;

namespace Maga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyCollectionChanged
    {
        private ObservableCollection<FilesDetails> _files = new();

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }
        public ObservableCollection<FilesDetails> Files
        {
            get => _files;
            set
            {
                _files = value;
                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Files));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(
                        // TextPointer to the start of content in the RichTextBox.
                        body.Document.ContentStart,
                        // TextPointer to the end of content in the RichTextBox.
                        body.Document.ContentEnd
                    );
            if(FileName.Text != "" && FontSizeee.Text != "")
            {
                if (Files.Count == 0) {

                    
                    Files.Add(new FilesDetails()
                    {
                        Name = FileName.Text,
                        FontSize = body.FontSize,
                        Text = textRange.Text
                    });
                }
                else
                {
                    Files.Add(Files[Files.Count - 1].Clone() as FilesDetails);
                    
                }
               
            }
        }
        private void FontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FontSizeee.Text != string.Empty && Convert.ToInt32(FontSizeee.Text) > 0)
                body.FontSize = Convert.ToDouble(FontSizeee.Text);
            else
            {
                FontSizeee.Text = "12";
                body.FontSize = 12;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
