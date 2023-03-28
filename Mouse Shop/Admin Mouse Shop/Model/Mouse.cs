using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Mouse_Shop.Model
{
    [Serializable]
    public class Mouse
    {
        public string ImagePath { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
        public int? DPI { get; set; } = null;
        public bool Wireless { get; set; }
        public bool Gaming { get; set; }
        public float? Price { get; set; } = null;
        public int Id { get; set; }
        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }
    }
}
