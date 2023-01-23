using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    internal class ShipPiece
    {
        private utils.Coodonnees _position;
        private bool _isHit;

        public ShipPiece(utils.Coodonnees position)
        {
            _position = position;
            _isHit = false;
        }

        public bool IsHit
        {
            get { return _isHit; }
            set { _isHit = value; }
        }

        public utils.Coodonnees Position
        {
            get { return _position; }

        }
    }
}
