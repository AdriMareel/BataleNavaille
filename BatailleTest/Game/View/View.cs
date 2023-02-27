using BatailleTest.Game.entity;
using BatailleTest.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.View
{
    internal class View
    {
        private Game game;

        public View(Game game)
        {
            this.game = game;
        }

        // function that returns a 2D array of string
        public string[,] get2DBoard(Board board)
        {
            string[,] tmpBoard = new string[game.GameRules.MapSize, game.GameRules.MapSize];
            foreach(Ship ship in board.PlayerShips) 
            {
                foreach(ShipPiece shipPiece in ship.ShipPieces) 
                {
                    tmpBoard[shipPiece.Position.X, shipPiece.Position.Y] = "ship";
                }
            }
            foreach(Hit hit in board.PlayerShots)
            {
                // hit de l'opponent
            }
            return tmpBoard;
        }

        // function on hover case to display incoming ship
        public List<Coordinates> hoverCase(Coordinates coords, Ship ship) 
        {
            if (ship.IsVertical && coords.Y + ship.Size > game.GameRules.MapSize)
            {
                coords.Y = game.GameRules.MapSize - ship.Size-1;
            } 
            
            else if(coords.X + ship.Size > game.GameRules.MapSize)
            {
                coords.X = game.GameRules.MapSize - ship.Size-1;
            }
            List<Coordinates> tmpShip = new List<Coordinates>();
            for(var i = 0; i<ship.Size;i++)
            {
                tmpShip.Add(coords);
                if (ship.IsVertical)
                {
                    coords.X += 1;
                }
                else
                {
                    coords.Y+= 1;
                }
            }
            return tmpShip;
        }

    }
}
