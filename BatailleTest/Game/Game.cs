using BatailleTest.Game.entity;
using BatailleTest.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game
{
    internal class Game
    {
        private GameRules _gameRules;

        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;
        private Player _otherPlayer;

        private Board _playerOneBoard;
        private Board _playerTwoBoard;

        private int _turn;


        public Game(GameRules gameRules = null, string player1Name = "Player1", string player2Name = "Player2")
        {
            _gameRules = gameRules ?? new GameRules();

            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
            _currentPlayer = _player1;
            _otherPlayer = _player2;
            _turn = 0;

            _playerOneBoard = new Board(_player1);
            _playerTwoBoard = new Board(_player2);
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
            GameRules gameRules = this.GameRules;
            
            if (player.AddShip(ship, GameRules))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddAShot(utils.Coordinates coordinates)
        {
            GameRules gameRules = this.GameRules;
            if(_currentPlayer == _player1)
            {
                if(_currentPlayer.AddShot(coordinates,gameRules, _playerTwoBoard) == "notValid")
                {
                    return false;
                }
            }
            else if(_currentPlayer == _player2)
            {
                if(_currentPlayer.AddShot(coordinates,gameRules,_playerOneBoard) == "notValid")
                {
                    return false;
                }
            }
            else { return false; }
            return true;
        }

    }
}
