using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [Serializable]
    public class FieldModel
    {
        public bool HasMine { get; set; }
        public bool IsOpened { get; set; }
        public bool HasFlag { get; set; }
        public int NumberOfMinesAround { get; set; }
    }
}
