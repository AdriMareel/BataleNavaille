using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatailleTest.DATA;
using Windows.ApplicationModel.VoiceCommands;

namespace BatailleTest.utils
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A 2d coordinates. </summary>


    internal class Coordinates
    {
        private int _x;
        private int _y;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="x">    (Optional) The x coordinate. </param>
        /// <param name="y">    (Optional) The y coordinate. </param>

        public Coordinates(int x = 0, int y = 0)
        {
            _x = x;
            _y = y;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the x coordinate. </summary>
        ///
        /// <value> The x coordinate. </value>

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the y coordinate. </summary>
        ///
        /// <value> The y coordinate. </value>

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Equality operator. </summary>
        ///
        /// <param name="c1">   The first instance to compare. </param>
        /// <param name="c2">   The second instance to compare. </param>
        ///
        /// <returns>   True if the coordonates are the same; otherwise, false </returns>

        public static bool operator ==(Coordinates c1, Coordinates c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Inequality operator. </summary>
        ///
        /// <param name="c1">   The first instance to compare. </param>
        /// <param name="c2">   The second instance to compare. </param>
        ///
        /// <returns>   False if the coordonates are the same; otherwise, true </returns>

        public static bool operator !=(Coordinates c1, Coordinates c2)
        {
            return !(c1 == c2);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Determines whether the specified object is equal to the current object. </summary>
        ///
        /// <param name="obj">  The object to compare with the current object. </param>
        ///
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Serves as the default hash function. </summary>
        ///
        /// <returns>   A hash code for the current object. </returns>

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Set the coordinates to random values. </summary>
        ///
        /// <param name="min">  (Optional) The minimum. </param>
        /// <param name="max">  (Optional) The maximum. </param>

        public void Randomize(int min = 0, int max = 9)
        {
            Random rnd = new Random();
            _x = rnd.Next(min, max-1);
            _y = rnd.Next(min, max-1);
        }
    }
}
