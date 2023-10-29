using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maga
{
    public class FilesDetails : ICloneable
    {
        public string Name { get; set; }
        public double? FontSize { get; set; }
        public string Text { get; set; }

        public object Clone()
        {
            return new FilesDetails()
            {
                Name = this.Name,
                FontSize = this.FontSize,
                Text = this.Text
            };
        }
    }
}
