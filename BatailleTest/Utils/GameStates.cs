using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.utils
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Add a type string that can only be egal to (warmup, playing, end) </summary>
    internal class GameStates
    {
        public enum States
        {
            Warmup,
            Playing,
            End,
        }


        private States _state;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="state">    (Optional) The state. </param>

        public GameStates(States state = States.Warmup)
        {
            _state = state;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the state. </summary>
        ///
        /// <value> The state. </value>

        public States State
        {
            get { return _state; }
            set { _state = value; }
        }

    }
}
