using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_Shop.Services.Interfaces
{
    interface ISerializeService
    {
        string Serialize<T>(object obj);
        T Deserialize<T>(string json);
    }
}
