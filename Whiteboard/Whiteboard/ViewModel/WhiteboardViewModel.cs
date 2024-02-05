using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Whiteboard.Services.Interfaces;
using Whiteboard.Model;
using System.Windows;
using System.Net.Mail;
using System.Net;

namespace Whiteboard.ViewModel
{
    public class WhiteboardViewModel : ViewModelBase
    {
        private readonly IMyNavigationService _myNavigationService;
        private readonly IUserManageService _userManageService;
        private readonly IProjectManageService _projectManageService;

        public string ProjectName { get; set; } = string.Empty;

        public WhiteboardViewModel(IMyNavigationService myNavigationService, IUserManageService userManageService, IProjectManageService projectManageService)
        {
            _myNavigationService = myNavigationService;
            _userManageService = userManageService;
            _projectManageService = projectManageService;
        }

        public RelayCommand<Canvas> OpenCommand
        {
            get => new(canvas =>
            {
                _myNavigationService.NavigateTo<HomeViewModel>();
            });
        }

        public RelayCommand<Canvas> SaveCommand
        {
            get => new(canvas =>
            {
                if (ProjectName != string.Empty)
                {
                    _projectManageService.AddProject(canvas, ProjectName);

                    _myNavigationService.NavigateWarning<HomeViewModel>(true);
                }
                else
                {
                    MessageBox.Show("You did not enter the project name!!!");
                }
            });
        }

        public RelayCommand<Canvas> ExportCommand
        {
            get => new(param =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Images (*.png)|*.png|JPEG Images (*.jpg)|*.jpg|All Files (*.*)|*.*";
                
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)param.ActualWidth, (int)(param.ActualHeight + 150), 96, 96, PixelFormats.Pbgra32);
                    renderBitmap.Render(param);

                    BitmapEncoder encoder = null;

                    if (filePath.EndsWith(".png"))
                    {
                        encoder = new PngBitmapEncoder();
                    }
                    else if (filePath.EndsWith(".jpg"))
                    {
                        encoder = new JpegBitmapEncoder();
                    }

                    if (encoder != null)
                    {
                        encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                        
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            encoder.Save(fs);
                        }
                    }
                }
            });
        }

        public RelayCommand<Canvas> SendCommand
        {
            get => new(canvas =>
            {
                UserModel user = _userManageService.GetCurrentUser();
                
                MailAddress toAddress = new(user.Email);
                MailAddress fromAddress = new("morujov48@gmail.com", "Whiteboard");

                MailMessage mailMessage = new(fromAddress, toAddress);

                mailMessage.Subject = $"Your project: {ProjectName}";

                RenderTargetBitmap renderBitmap = new((int)(canvas.ActualWidth), (int)(canvas.ActualHeight + 150), 96, 96, PixelFormats.Pbgra32);
                renderBitmap.Render(canvas);

                BitmapSource canvasImage = renderBitmap;

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (var imageStream = new MemoryStream())
                {
                    encoder.Save(imageStream);

                    imageStream.Position = 0;

                    var imageAttachment = new Attachment(imageStream, "project.png");
                    mailMessage.Attachments.Add(imageAttachment);

                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = $"<html><body><h2>{user.Username}</h2><img src='https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.PdiyshwX9HujD06tG9TXMQHaFj%26pid%3DApi&f=1&ipt=ebbe9e13480c3c335c87913de376bad04aeafb8aba7b258f35172219b37be2ff&ipo=images'></img></body></html>";

                    SmtpClient smtpClient = new("smtp.gmail.com", 587);
                    smtpClient.Credentials = new NetworkCredential("morujov48@gmail.com", "bmpfvpyyripiiwey");
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mailMessage);
                }

                MessageBox.Show("Mail send successfully");
            });
        }

        public RelayCommand<Canvas> PrintCommand
        {
            get => new(canvas =>
            {
                PrintDialog printDialog = new();
                
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(canvas, "Canvas Print");
                }
            });
        }
    }
}
