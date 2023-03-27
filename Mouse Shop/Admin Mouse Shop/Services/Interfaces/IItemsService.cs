using Admin_Mouse_Shop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.Services.Interfaces
{
    interface IItemsService
    {
        public void Add(Mouse item);
        public int Find(Mouse item);
        public void Edit(string Model, string Company);
        public void Set(Mouse PrevProduct, Mouse NewProduct);
        public void Delete(string Model, string Company);
    }
}
