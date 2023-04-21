using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy_Pattern
{
    class CarProxy : ICar
    {
        private Car car;
        private int FuelLevel = 100;
        public void Drive()
        {
            if (car == null)
            {
                car = new Car();
            }

            Console.WriteLine("Starting engine...");
            car.Drive();
        }
    }
}
