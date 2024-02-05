using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using Whiteboard.Context;
using Whiteboard.Model;
using Whiteboard.Services.Interfaces;
using FluentFTP;
using System.Net;
using System.IO;
using System.Data.Entity.Core.Metadata.Edm;
using Azure.Core.Diagnostics;
using System.Windows;

namespace Whiteboard.Services.Classes
{
    public class ProjectManageService : IProjectManageService
    {
        private readonly WhiteboardDbContext _context;

        public ProjectManageService(WhiteboardDbContext context)
        {
            _context = context;
        }

        public bool CheckProjectExist(string projectName)
        {
            return _context.Pictures.Any(x => x.ProjectName == projectName);
        }

        public void AddProject(Canvas canvas, string projectName)
        {
            if(!CheckProjectExist(projectName))
            {
                SketchModel tmpPicture = new SketchModel();
                tmpPicture.DateCreated = DateTime.Now;
                tmpPicture.ProjectName = projectName;

                var renderBitmap = new RenderTargetBitmap((int)canvas.ActualWidth, (int)(canvas.ActualHeight + 150), 96, 96, PixelFormats.Default);
                renderBitmap.Render(canvas);

                BitmapSource canvasImage = renderBitmap;

                using (var client = new FtpClient("ftp://ch02d0f503_murdexx:murad2212@auris.cityhost.com.ua"))
                {
                    client.Credentials = new NetworkCredential("ch02d0f503_murdexx", "murad2212");
                    client.Connect();

                    using (var imageStream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(canvasImage));
                        encoder.Save(imageStream);

                        imageStream.Position = 0;

                        client.UploadStream(imageStream, $"/{projectName}.png");
                    }
                }

                tmpPicture.ProjectLink = $"https://telegram-bots-maga.cx.ua/Murdexx/{projectName}.png";

                _context.Pictures.Add(tmpPicture);
                _context.SaveChanges();
            }
            else if (CheckProjectExist(projectName))
            {
                MessageBox.Show("This projectName is already used!");
            }
        }

        public void DeleteProject(SketchModel picture)
        {
            if (CheckProjectExist(picture.ProjectName))
            {
                var pictureToDelete = _context.Pictures.Find(picture.Id);

                _context.Pictures.Remove(pictureToDelete);
                _context.SaveChanges();

                using (var client = new FtpClient("ftp://ch02d0f503_murdexx:murad2212@auris.cityhost.com.ua", "ch02d0f503_murdexx", "murad2212"))
                {
                    client.Connect();

                    if (client.FileExists($"/{picture.ProjectName}.png"))
                    {
                        client.DeleteFile($"/{picture.ProjectName}.png");
                        MessageBox.Show($"{picture.ProjectName} is already deleted!");
                    }
                }
            }
        }
    }
}
