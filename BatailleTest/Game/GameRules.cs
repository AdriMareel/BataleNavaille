using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatailleTest.DATA;

namespace BatailleTest.Game
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A Class taht contains every rules of a game. </summary>

    internal class GameRules
    {
        private int _nbShip;
        private Dictionary<String, int> _shipList;
        private int MAX_SHIP = 5;
        private int MIN_SHIP = 5;

        private int _mapSize;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <exception cref="Exception">    Thrown when values are not possible. </exception>
        ///
        /// <param name="mapSize">  (Optional) Size of the map. </param>
        /// <param name="nbShip">   (Optional) The nb ship. </param>
        /// <param name="shipList"> (Optional) List of ships. </param>

        public GameRules(int? mapSize = null, int? nbShip = null, Dictionary<String, int> shipList = null)
        {
            _mapSize = mapSize??Const.GRID_SIZE;
            _shipList = shipList??DATA.DefaultShip.DefaultShipList;
            _nbShip =  nbShip??DATA.DefaultShip.NumberOfShip;

            if (mapSize < 0)
            {
                throw new Exception("Map size must be greater than 0");
            }
            if (_nbShip != _shipList.Count)
            {
                throw new Exception("Number of ship must be equal to the number of ship in the ship list");
            }
            if (_nbShip > MAX_SHIP || _nbShip < MIN_SHIP)
            {
                throw new Exception("Number of ship must be between " + MIN_SHIP + " and " + MAX_SHIP);
            }

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the nb ship. </summary>
        ///
        /// <value> The nb ship. </value>

        public int NbShip
        {
            get { return _nbShip; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the maximum ship number. </summary>
        ///
        /// <value> The maximum ship. </value>

        public int MaxShip
        {
            get { return MAX_SHIP; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a list of ships that should be in the game. </summary>
        ///
        /// <value> A list of ships. </value>

        public Dictionary<String, int> ShipList
        {
            get { return _shipList; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the map size. </summary>
        ///
        /// <value> The size of the map. </value>

        public int MapSize
        {
            get { return _mapSize; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a ship to the rules. </summary>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="shipName"> Name of the ship. </param>
        /// <param name="size">     The size. </param>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Determine if the rules are valid. </summary>
        ///
        /// <returns>   True if rules valid, false if not. </returns>

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
