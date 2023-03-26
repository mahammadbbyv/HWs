using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Model
{
    public class Mouse
    {
        public string ImagePath { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
        public int DPI { get; set; }
        public bool Wireless { get; set; }
        public bool Gaming { get; set; }
        public float Price { get; set; }
    }
}
