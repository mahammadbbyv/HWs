using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Method
{
    public class ElectricCar : Car
    {
        protected override void TurningOn()
        {
            Console.WriteLine("Starting electric engine");
        }
    }
}
