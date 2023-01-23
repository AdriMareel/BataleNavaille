using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    internal class Board
    {
        private int _size;
        private List<List<BoardCase>> _cases;
        public Board()
        {
            _size = 10;
            _cases = new List<List<BoardCase>>();
            for (int i = 0; i < _size; i++)
            {
                _cases.Add(new List<BoardCase>());
                for (int j = 0; j < _size; j++)
                {
                    //_cases[i].Add(new BoardCase(new utils.Coodonnees(i, j)));
                }
            }
        }
    }
}
