using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter_Pattern
{
    interface Engine
    {
        public void Attach();
        public void Deattach();
        public void Start();
        public void Stop();
    }
}
