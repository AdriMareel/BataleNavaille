using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.utils
{
    //Add a type string that can only be egal to (warmup, playing, end)
    internal class GameStates
    {
        private string _state;

        public GameStates(string state = "warmup")
        {
            if (state == "warmup" || state == "playing" || state == "end")
            {
                _state = state;
            }
            else
            {
                throw new Exception("Invalid state");
            }
        }

        public string State
        {
            get { return _state; }
        }

        public void setWarmup()
        {
            _state = "warmup";
        }

        public void setPlaying()
        {
            _state = "playing";
        }

        public void setEnd()
        {
            _state = "end";
        }

        public static bool operator ==(GameStates g1, GameStates g2)
        {
            return g1.State == g2.State;
        }

        public static bool operator !=(GameStates g1, GameStates g2)
        {
            return !(g1 == g2);
        }

    }
}
