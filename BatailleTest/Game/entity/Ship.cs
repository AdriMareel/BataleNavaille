using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    internal class Ship
    {
        private int _size;
        private bool _isVertical;
        private utils.Coodonnees _startPosition;
        private List<ShipPiece> _shipPieces;

        private int _life;
        private bool _isAlive;

        public Ship(utils.Coodonnees startPosition, int size, bool isVertical = true)
        {
            _startPosition = startPosition;
            _size = size;
            _life = size;
            _isVertical = isVertical;
            _isAlive = true;

            generateBoatPieces();
        }


        private void generateBoatPieces()
        {
            _shipPieces = new List<ShipPiece>();
            for (int i = 0; i < _size; i++)
            {
                if (_isVertical)
                {
                    _shipPieces.Add(new ShipPiece(new utils.Coodonnees(_startPosition.X, _startPosition.Y + i)));
                }
                else
                {
                    _shipPieces.Add(new ShipPiece(new utils.Coodonnees(_startPosition.X + i, _startPosition.Y)));
                }
            }
        }
        public bool IsAlive
        {
            get { return _isAlive; }
        }

        public bool IsVertical
        {
            get { return _isVertical; }
        }

        public int Size
        {
            get { return _size; }
        }

        public utils.Coodonnees StartPosition
        {
            get { return _startPosition; }
        }

        public List<ShipPiece> ShipPieces
        {
            get { return _shipPieces; }
        }

        public void Hit(utils.Coodonnees position)
        {
            foreach (ShipPiece boatPiece in _shipPieces)
            {
                if (boatPiece.Position.X == position.X && boatPiece.Position.Y == position.Y)
                {
                    boatPiece.IsHit = true;
                    _life--;
                    if (_life == 0)
                    {
                        _isAlive = false;
                    }
                }
            }
        }

    }
}
