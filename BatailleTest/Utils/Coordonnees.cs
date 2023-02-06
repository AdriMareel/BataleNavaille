using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Coordinates c = obj as Coordinates;
            if ((System.Object)c == null)
            {
                return false;
            }

            return (X == c.X) && (Y == c.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public void Randomize(int min = 0, int max = 10)
        {
            Random rnd = new Random();
            _x = rnd.Next(min, max-1);
            _y = rnd.Next(min, max-1);
        }
    }
}
