using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatailleTest.Game.entity;
using BatailleTest.utils;
using Windows.Graphics.Printing.Workflow;
using Windows.Media.Devices.Core;

namespace BatailleTest.DATA
{
    internal class DefaultShip
    {
        private const int CARRIER_SIZE = 5;
        private const int BATTLESHIP_SIZE = 4;
        private const int CRUISER_SIZE = 3;
        private const int SUBMARINE_SIZE = 3;
        private const int DESTROYER_SIZE = 2;

        private Dictionary<String, int> DefaultShipList = new Dictionary<String, int>(
            new List<KeyValuePair<String, int>>()
            {
                new KeyValuePair<String, int>("Carrier", CARRIER_SIZE),
                new KeyValuePair<String, int>("Battleship", BATTLESHIP_SIZE),
                new KeyValuePair<String, int>("Cruiser", CRUISER_SIZE),
                new KeyValuePair<String, int>("Submarine", SUBMARINE_SIZE),
                new KeyValuePair<String, int>("Destroyer", DESTROYER_SIZE)
            }
        );

        public Dictionary<String, int> DefaultShipListGetter
        {
            get { return DefaultShipList; }
        }
    }
}
