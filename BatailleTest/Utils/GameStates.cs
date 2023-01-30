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
        public enum States
        {
            Warmup,
            Playing,
            End,
        }


        private States _state;

        public GameStates(States state = States.Warmup)
        {
            _state = state;
        }

        public States State
        {
            get { return _state; }
            set { _state = value; }
        }

    }
}
