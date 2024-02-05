using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Whiteboard.Model;

namespace Whiteboard.Services.Interfaces
{
    public interface IProjectManageService
    {
        public void AddProject(Canvas canvas, string projectName);
        public void DeleteProject(SketchModel picture);
        public bool CheckProjectExist(string projectName);
    }
}
