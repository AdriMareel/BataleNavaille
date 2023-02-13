using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatailleTest.DATA;

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
            bool[,] board = new bool[Const.GRID_SIZE, Const.GRID_SIZE];

            foreach (Ship ship in _playerShips)
            {
                foreach (ShipPiece boatPiece in ship.ShipPieces)
                {
                    try
                    {
                        if(boatPiece.Position.X <= Const.GRID_SIZE && boatPiece.Position.Y <= Const.GRID_SIZE)
                        {
                            board[boatPiece.Position.X, boatPiece.Position.Y] = true;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }


            Debug.WriteLine(" ABCDEFGHIJ");
            for (int i = 0; i < Const.GRID_SIZE; i++)
            {
                Debug.Write(i );
                for (int j = 0; j < Const.GRID_SIZE; j++)
                {
                    Debug.Write((board[i, j]? "*" : "_"));
                }
                Debug.Write("\n");
            }
        }
    }
}
