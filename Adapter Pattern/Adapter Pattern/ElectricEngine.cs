using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter_Pattern
{
    class ElectricEngine : Engine
    {
        public void Attach()
        {
            Console.WriteLine("Attahing an electric engine...");
        }
        public void Deattach()
        {
            Console.WriteLine("Deattahing an electric engine...");
        }
        public void Start()
        {
            Console.WriteLine("Starting an electric engine...");
        }
        public void Stop()
        {
            Console.WriteLine("Stopping an electric engine...");
        }

        public ElectricEngine()
        {
            Attach();
        }
    }
}
