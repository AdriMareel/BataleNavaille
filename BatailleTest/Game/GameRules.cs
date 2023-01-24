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
        private Dictionary<String, int> _shipList = new Dictionary<String, int>();
        private int MAX_SHIP = 5;
        private int MIN_SHIP = 5;

        private int _mapSize;


        public GameRules(int mapSize = 10, int nbShip = 0, Dictionary<String, int> shipList = null)
        {
            if (shipList == null)
            {
                shipList = DATA.DefaultShip.DefaultShipList;
            }


            if (mapSize < 0)
            {
                throw new Exception("Map size must be greater than 0");
            }
            if (nbShip != shipList.Count)
            {
                throw new Exception("Number of ship must be equal to the number of ship in the ship list");
            }
            if (nbShip > MAX_SHIP || nbShip < MIN_SHIP)
            {
                throw new Exception("Number of ship must be between " + MIN_SHIP + " and " + MAX_SHIP);
            }

            _mapSize = mapSize;
            _nbShip = nbShip;
            _shipList = shipList;
        }

        public int NbShip
        {
            get { return _nbShip; }
        }

        public Dictionary<String, int> ShipList
        {
            get { return _shipList; }
        }

        public int MapSize
        {
            get { return _mapSize; }
        }

        public void addShip(string shipName, int size)
        {
            if (_nbShip + 1 > MAX_SHIP)
            {
                throw new Exception("There is already " + MAX_SHIP + " ships");
            }

            if (_shipList.ContainsKey(shipName))
            {
                throw new Exception("Ship name already exist");
            }

            if (_shipList.Values.Sum() + size > _mapSize * _mapSize)
            {
                throw new Exception("The sum of all ship size must be less than the map size");
            }

            if (shipName == null || shipName == "")
            {
                throw new Exception("Ship name must not be null or empty");
            }

            if (size < 0)
            {
                throw new Exception("Ship size must be greater than 0");
            }

            _nbShip++;
            _shipList.Add(shipName, size);
        }

        public bool areRulesValid()
        {
            if (_nbShip != _shipList.Count)
            {
                return false;
            }
            if (_nbShip > MAX_SHIP || _nbShip < MIN_SHIP)
            {
                return false;
            }

            if (_shipList.Values.Sum() > _mapSize * _mapSize)
            {
                return false;
            }

            return true;
        }
    }
}
