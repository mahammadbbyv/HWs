using Admin_Mouse_Shop.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.ViewModel
{
    class EditViewModel : ViewModelBase
    {
        public string Company { get; set; }
        public string Model { get; set; }
        private readonly IItemsService _itemsService;
        public EditViewModel(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }
        public RelayCommand EditCommand
        {
            get => new(() =>
            {
                _itemsService.Edit(Company, Model);
            });
        }
    }
}
