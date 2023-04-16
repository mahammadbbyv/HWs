using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Pattern
{
    public class Car
    {
        private readonly Engine engine = new();
        private readonly Battery battery = new();
        public void Start()
        {
            engine.Start();
            battery.On();
        }
        public void Stop()
        {
            battery.Off();
            engine.Stop();
        }
    }
}
