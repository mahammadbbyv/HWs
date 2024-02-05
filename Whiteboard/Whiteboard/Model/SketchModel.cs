using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Model
{
    public class SketchModel
    {
        [Key]
        public int Id { get; set; }

        public string ProjectLink { get; set; } 
        public DateTime DateCreated { get; set; }
        public string ProjectName { get; set; }
    }
}
