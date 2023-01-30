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

        private GameStates _gameState;



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

        public Board PlayerOneBoard
        {
            get { return _playerOneBoard; }
        }

        public Board PlayerTwoBoard
        {
            get { return _playerTwoBoard; }
        }

        public void Start()
        {
            //TODO
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

        public void PlayTurn(Coordinates coordinates, Player player = null)
        {
            if (player == null)
                player = _currentPlayer;
            
            
            if (_gameState.State == GameStates.States.Warmup)
            {
                //TODO call this.AddAShip(player); /!\ pour l 'instant ca va pas marcher car il faut que je fasse un truc pour savoir quel bateau est a etre placé
                // if tous les joueurs ont tous leurs ships set gamestate to playing
            }
            else if (_gameState.State == GameStates.States.Playing)
            {
                //TODO call this.AddAShot(Coordinates); + switch player + next turn
                // if this.isOver() then set Gamestate to end

            }
            else if (_gameState.State == GameStates.States.End)
            {
                //TODO
            }
        }
        
        private bool AddAShip(Player player, Ship ship)
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

        private bool AddAShot(Coordinates coordinates)
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

        public bool IsOver()
        {
            if (this.GetWinner() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Player GetWinner()
        {
            if (_player1.isAShipAlive() && !_player2.isAShipAlive())
            {
                return _player1;
            }
            else if (!_player1.isAShipAlive() && _player2.isAShipAlive())
            {
                return _player2;    
            }
            else { return null; }
        }       

    }
}
