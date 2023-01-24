using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    internal class Player
    {
        private string _name;
        private int _score;
        private List<Ship> _ships;

        private List<Hit> _playerShots;

        public Player(string name)
        {
            _name = name;
            _score = 0;
            _ships = new List<Ship>();
            _playerShots = new List<Hit>();
        }

        public string Name
        {
            get { return _name; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public List<Ship> Ships
        {
            get { return _ships; }
        }

        public int getNumberOfShip()
        {
            return _ships.Count;
        }

        public List<Hit> PlayerShots
        {
            get { return _playerShots; }
        }

        public bool isAShipAt(utils.Coodonnees position)
        {
            foreach (Ship ship in _ships)
            {
                foreach (ShipPiece boatPiece in ship.ShipPieces)
                {
                    if (boatPiece.Position.X == position.X && boatPiece.Position.Y == position.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool doesShipFit(Ship ship)
        {
            foreach (ShipPiece shipPiece in ship.ShipPieces)
            {
                //TODO : change tmpSize to the game's board size
                var tmpSize = 10;
                if (shipPiece.Position.X > tmpSize || shipPiece.Position.Y > tmpSize || shipPiece.Position.X < 0 || shipPiece.Position.Y < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isAShipAlive()
        {
            foreach (Ship ship in _ships)
            {
                if (ship.IsAlive)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddShip(Ship ship, GameRules rules)
        {
            //todo : check if the number of ship is not greater than the number of ship allowed
            //todo : check is the ship is not out of the board

            foreach (ShipPiece shipPiece in ship.ShipPieces)
            {
                if (isAShipAt(shipPiece.Position))
                {
                    return false;
                }
            }
            _ships.Add(ship);
            return true;
        }

        public bool isAShotAt(utils.Coodonnees position)
        {
            foreach (Hit hit in _playerShots)
            {
                if (hit.Position.X == position.X && hit.Position.Y == position.Y)
                {
                    return true;
                }
            }
            return false;
        }


        public void addShot(Hit hit)
        {
            _playerShots.Add(hit);
        }

    }
}
