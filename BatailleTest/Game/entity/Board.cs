using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    internal class Board
    {
        private List<Ship> _playerShips;
        private List<Hit> _playerShots;


        public Board(Player player)
        {
            _playerShots = player.PlayerShots;
            _playerShips = player.Ships;

        }

        public List<Ship> PlayerShips
        {
            get { return _playerShips; }
        }

        public List<Hit> PlayerShots
        {
            get { return _playerShots; }
        }
    }
}
