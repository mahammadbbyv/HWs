using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Pattern
{
    class Engine
    {
        public void Start()
        {
            Console.WriteLine("Starting the engine...");
        }

        public void Stop()
        {
            Console.WriteLine("Stoppin the engine...");
        }
    }
}
