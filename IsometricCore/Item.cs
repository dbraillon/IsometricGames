using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric.Core
{
    public class Item
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Size { get; set; }

        public Item(int x, int y, int z, int size)
        {
            X = x;
            Y = y;
            Z = z;
            Size = size;
        }
    }
}
