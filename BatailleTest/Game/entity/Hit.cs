using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    internal class Hit
    {
        private utils.Coodonnees _position;
        private bool _isShipPiece;
        private ShipPiece _shipPiece;
        private string _status;


        public Hit(utils.Coodonnees position, string status,bool isShipPiece = false, ShipPiece shipPiece = null)
        {
            _position = position;
            _isShipPiece = isShipPiece;
            _shipPiece = shipPiece;
            _status = status;
        }

        public utils.Coodonnees Position
        {
            get { return _position; }
        }

        public bool IsShipPiece
        {
            get { return _isShipPiece; }
        }

        public ShipPiece ShipPiece
        {
            get { return _shipPiece; }
        }

        public string Status
        {
            get { return _status; }
        }

    }
}
