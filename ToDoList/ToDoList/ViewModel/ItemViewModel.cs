using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.ViewModel
{
    class ItemViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        public string ToDoName { get; set; }
        public void ReceiveMessage(ToDos message)
        {
            ToDoName = message.Name;
        }

        public ItemViewModel(IMessenger messenger)
        {

            _messenger = messenger;
            _messenger.Register<ToDos>(this, ReceiveMessage);
        }
    }
}
