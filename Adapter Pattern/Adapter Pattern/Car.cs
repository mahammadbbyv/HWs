using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter_Pattern
{
    class Car
    {
        private readonly Engine engine;
        public Car(Engine _engine)
        {
            if (engine == null)
                engine = _engine;
            else
                Console.WriteLine("Deattach car's engine first!");
        }
        public void Start()
        {
            engine.Start();
        }
        public void Stop()
        {
            engine.Stop();
        }
        public void Deattach()
        {
            engine.Deattach();
        }
    }
}
