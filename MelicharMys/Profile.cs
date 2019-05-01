using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelicharMys
{
    public class Profile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MouseSpeed { get; set; }
        public int ScrollSpeed { get; set; }
        public int DoubleClickTime { get; set; }
        public bool FromDB { get; set; }
    }
}
