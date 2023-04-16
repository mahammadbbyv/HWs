using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Method
{
    public class PetrolCar : Car
    {
        protected override void TurningOn()
        {
            Console.WriteLine("Starting petrol engine");
        }
    }
}
