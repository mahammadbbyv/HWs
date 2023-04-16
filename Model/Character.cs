using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create_Character.Model
{
    public class Character
    {
#pragma warning disable CS8618
        public string Name { get; set; }
        public int Power { get; set; }
        public string Group { get; set; }
#pragma warning restore CS8618
    }
}
