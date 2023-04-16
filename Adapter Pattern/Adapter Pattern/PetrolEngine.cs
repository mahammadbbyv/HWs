using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter_Pattern
{
    class PetrolEngine : Engine
    {
        public void Attach()
        {
            Console.WriteLine("Attahing a petrol engine...");
        }
        public void Start()
        {
            Console.WriteLine("Starting a petrol engine...");
        }
        public void Stop()
        {
            Console.WriteLine("Stopping a petrol engine...");
        }
        public PetrolEngine()
        {
            Attach();
        }
    }
}
