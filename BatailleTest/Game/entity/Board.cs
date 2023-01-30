using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    internal class Board
    {
        private List<Ship> _playerOneShips;
        private List<Hit> _playerOneShots;

        private List<Ship> _playerTwoShips;
        private List<Hit> _playerTwoShots;

        public Board(Player playerOne, Player PlayerTwo)
        {
            _playerOneShips = playerOne.Ships;
            _playerOneShots = playerOne.PlayerShots;

            _playerTwoShips = PlayerTwo.Ships;
            _playerTwoShots = PlayerTwo.PlayerShots;
        }
    }
}
