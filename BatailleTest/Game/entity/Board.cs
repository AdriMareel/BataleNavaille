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

        public void DebugPlayerShipsDisplay()
        {
            bool[,] board = new bool[10, 10];

            foreach (Ship ship in _playerShips)
            {
                foreach (ShipPiece boatPiece in ship.ShipPieces)
                {
                    board[boatPiece.Position.X, boatPiece.Position.Y] = true;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write((board[i, j]? "X" : "0"));
                }
                Console.WriteLine();
            }
        }
    }
}
