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

        public int getIndexOfShipAt(utils.Coodonnees position)
        {
            for (int i = 0; i < _ships.Count; i++)
            {
                foreach (ShipPiece boatPiece in _ships[i].ShipPieces)
                {
                    if (boatPiece.Position.X == position.X && boatPiece.Position.Y == position.Y)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public bool doesShipFit(Ship ship, GameRules rules)
        {
            foreach (ShipPiece shipPiece in ship.ShipPieces)
            {
                var boardSize = rules.MapSize;
                if (shipPiece.Position.X > boardSize || shipPiece.Position.Y > boardSize || shipPiece.Position.X < 0 || shipPiece.Position.Y < 0)
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
            if(_ships.Count == rules.MaxShip)
            {
                return false;
            }
            
            foreach (ShipPiece shipPiece in ship.ShipPieces)
            {
                if (isAShipAt(shipPiece.Position) || doesShipFit(ship, rules))
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


        public void addShot(Hit hit, GameRules rules)
        {
            //todo : check if the number of shot is not greater than the number of shot allowed (map size)²
            if(_playerShots.Count >= (rules.MapSize^2))
            {
                return;
            }
            //todo : check if the shot is not out of the board
            if(hit.Position.X < 0 || hit.Position.Y < 0 || hit.Position.X >= rules.MapSize || hit.Position.Y >= rules.MapSize)
            {
                return;
            }
            //todo: check if the shot is not already done
            if (_playerShots.Contains(hit)){
                return;
            }
            //todo: return  hit status (miss, hit, sunk, notValid)

            _playerShots.Add(hit);
            return;
        }

    }
}
