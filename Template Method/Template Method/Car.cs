using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Method
{
    public abstract class Car
    {
        public void Start()
        {
            Console.WriteLine("Starting the car");
            TurningOn();
            Console.WriteLine("Car started");
        }

        protected abstract void TurningOn();
    }
}
