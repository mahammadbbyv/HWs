using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.Context;
using Whiteboard.Model;
using Whiteboard.Services.Interfaces;
using Whiteboard.Services.Messages;

namespace Whiteboard.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<SketchModel> Projects { get; set; } = new ObservableCollection<SketchModel>();

        private readonly IMessenger _messenger;
        private readonly IMyNavigationService _myNavigationService;

        private readonly WhiteboardDbContext _context;

        public void ReceiveDataMessage(DataMessages message)
        {
            if ((bool)message.Data) 
            {
                Projects = new ObservableCollection<SketchModel>(_context.Pictures);
            }
        }

        public HomeViewModel(IMessenger messenger, IMyNavigationService myNavigationService, WhiteboardDbContext context)
        {
            _messenger = messenger;
            _myNavigationService = myNavigationService;
            _context = context;

            _messenger.Register<DataMessages>(this, ReceiveDataMessage);
        }

        public RelayCommand AddProjectCommand
        {
            get => new(() =>
            {
                _myNavigationService.NavigateTo<WhiteboardViewModel>();
            });
        }
    }
}
