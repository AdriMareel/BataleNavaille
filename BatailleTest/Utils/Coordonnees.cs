using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.utils
{
    internal class Coordinates
    {
        private int _x;
        private int _y;
        public Coordinates(int x = 0, int y = 0)
        {
            _x = x;
            _y = y;
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }


        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public static bool operator ==(Coordinates c1, Coordinates c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public static bool operator !=(Coordinates c1, Coordinates c2)
        {
            return !(c1 == c2);
        }

    }
}
