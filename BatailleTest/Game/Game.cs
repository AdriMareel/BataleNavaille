using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Printers;

namespace AppBataleNavaille.Game
{
    internal class Game
    {
        private GameRules _gameRules;

        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;
        private Player _otherPlayer;
        private int _turn;

        
        public Game(GameRules gameRules)
        {
            _gameRules = gameRules;

            _player1 = new Player("Player1");
            _player2 = new Player("Bot");
            _currentPlayer = _player1;
            _otherPlayer = _player2;
            _turn = 1;
        }

        public Player Player1
        {
            get { return _player1; }
        }

        public Player Player2
        {
            get { return _player2; }
        }

        public GameRules GameRules
        {
            get { return _gameRules; }
        }

        public int Turn
        {
            get { return _turn; }
        }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
        }

        public Player OtherPlayer
        {
            get { return _otherPlayer; }
        }

        public void Start()
        {
            Console.WriteLine("Game started");

        }

        private void SwitchPlayer()
        {
            if (_currentPlayer == _player1)
            {
                _currentPlayer = _player2;
                _otherPlayer = _player1;
            }
            else
            {
                _currentPlayer = _player1;
                _otherPlayer = _player2;
            }
        }

        private void NextTurn()
        {
            _turn++;
            SwitchPlayer();
        }

        public bool AddAShip(Player player, Ship ship)
        {
            if (player.getNumberOfShip() < _gameRules.NbShip)
            {
                if (player.AddShip(ship))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void GetBoard(Player player)
        {
            
        }
    }
}
