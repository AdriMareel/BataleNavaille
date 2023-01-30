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
        private string _name;
        private bool _isVertical;
        private utils.Coordinates _startPosition;
        private List<ShipPiece> _shipPieces;

        private int _life;
        private bool _isAlive;

        public Ship(utils.Coordinates startPosition,string name , int size, bool isVertical = true)
        {
            _startPosition = startPosition;
            _name = name;
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
                    _shipPieces.Add(new ShipPiece(new utils.Coordinates(_startPosition.X, _startPosition.Y + i)));
                }
                else
                {
                    _shipPieces.Add(new ShipPiece(new utils.Coordinates(_startPosition.X + i, _startPosition.Y)));
                }
            }
        }

        public string Name
        {
            get { return _name; }
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

        public utils.Coordinates StartPosition
        {
            get { return _startPosition; }
        }

        public List<ShipPiece> ShipPieces
        {
            get { return _shipPieces; }
        }

        public void Hit(utils.Coordinates position)
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
