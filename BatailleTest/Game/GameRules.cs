using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game
{
    internal class GameRules
    {
        private int _nbShip;
        private int[] _shipSize;
        private int MAX_SHIP = 5;
        private int MIN_SHIP = 5;

        private int _mapSize;


        public GameRules(int mapSize = 10, int nbShip = 0, int[] shipSize = null)
        {
            if (mapSize < 0)
            {
                throw new Exception("Map size must be greater than 0");
            }
            if (nbShip != shipSize.Length)
            {
                throw new Exception("Number of ship must be equal to the number of ship size");
            }
            if (nbShip > MAX_SHIP || nbShip < MIN_SHIP)
            {
                throw new Exception("Number of ship must be between " + MIN_SHIP + " and " + MAX_SHIP);
            }

            _mapSize = mapSize;
            _nbShip = nbShip;
            _shipSize = shipSize;
        }

        public int NbShip
        {
            get { return _nbShip; }
        }

        public int[] ShipSize
        {
            get { return _shipSize; }
        }

        public int MapSize
        {
            get { return _mapSize; }
        }

        public void addShip(int size)
        {
            if (_nbShip + 1 > MAX_SHIP)
            {
                throw new Exception("There is already " + MAX_SHIP + " ships");
            }

            if (size + _shipSize.Sum() > _mapSize * _mapSize)
            {
                throw new Exception("The sum of all ship size must be less than the map size");
            }

            _nbShip++;
            _shipSize = _shipSize.Concat(new int[] { size }).ToArray();
        }

        public bool areRulesValid()
        {
            if (_nbShip != _shipSize.Length)
            {
                return false;
            }
            if (_nbShip > MAX_SHIP || _nbShip < MIN_SHIP)
            {
                return false;
            }

            if (_shipSize.Sum() > _mapSize * _mapSize)
            {
                return false;
            }

            return true;
        }
    }
}
