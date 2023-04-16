using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_Pattern
{
    internal class SportCar : Car
    {
        public SportCar(Engine engine) : base(engine)
        {
        }
        public void Start()
        {
            Console.WriteLine("Sport's car engine started.");
        }

        public void Stop()
        {
            Console.WriteLine("Sport's car engine stopped.");
        }
    }
}
