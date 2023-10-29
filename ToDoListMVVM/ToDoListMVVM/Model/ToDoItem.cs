using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVVM.Model
{
    class ToDoItem
    {
        public string? Label { get; set; }
        public string? Short_Description { get; set; }
        public string? Long_Description { get; set; }
        public DateTime? AddeddDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
