using Admin_Mouse_Shop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.Messages
{
    public class Product : INotifyPropertyChanged
    {
#pragma warning disable CS8618
        public Mouse Mouse { get; set; }
#pragma warning restore CS8618
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _Count;
        public int Count
        {
            get { return _Count; }
            set
            {
                _Count = value;
                OnPropertyChange(nameof(Count));
            }
        }
    }
}
