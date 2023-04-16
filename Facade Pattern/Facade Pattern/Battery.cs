using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Pattern
{
    class Battery
    {
        public void On()
        {
            Console.WriteLine("Turning the battery on...");
        }

        public void Off()
        {
            Console.WriteLine("Turning the battery off...");
        }
    }
}
