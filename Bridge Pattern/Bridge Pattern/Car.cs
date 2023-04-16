using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_Pattern
{
    abstract class Car
    {
        protected Engine _engine;
        protected Car(Engine engine) 
        {
            _engine = engine;
        }

        public virtual void Start()
        {
            _engine.Start();
        }

        public virtual void Stop()
        {
            _engine.Stop();
        }
    }
}
