using BatailleTest.Game.entity;
using BatailleTest.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Documents;

namespace BatailleTest.Game
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Classe game. </summary>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="player1Name">  (Optional) Name of the player 1. </param>
        /// <param name="player2Name">  (Optional) Name of the player 2. </param>
        /// <param name="gameRules">    (Optional) The game rules. </param>

        public Game(string player1Name = "Player1", string player2Name = "Player2", GameRules gameRules = null)
        {
            _gameRules = gameRules ?? new GameRules();

            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
            _currentPlayer = _player1;
            _otherPlayer = _player2;
            _turn = 0;

            _playerOneBoard = new Board(_player1);
            _playerTwoBoard = new Board(_player2);

            _gameState = new GameStates(GameStates.States.Warmup);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the player 1. </summary>
        ///
        /// <value> The player 1. </value>

        public Player Player1
        {
            get { return _player1; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the player 2. </summary>
        ///
        /// <value> The player 2. </value>

        public Player Player2
        {
            get { return _player2; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the game rules. </summary>
        ///
        /// <value> The game rules. </value>

        public GameRules GameRules
        {
            get { return _gameRules; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the turn. </summary>
        ///
        /// <value> The turn. </value>

        public int Turn
        {
            get { return _turn; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the current player. </summary>
        ///
        /// <value> The current player. </value>

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the other player. </summary>
        ///
        /// <value> The other player. </value>

        public Player OtherPlayer
        {
            get { return _otherPlayer; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the player one board. </summary>
        ///
        /// <value> The player one board. </value>

        public Board PlayerOneBoard
        {
            get { return _playerOneBoard; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the player two board. </summary>
        ///
        /// <value> The player two board. </value>

        public Board PlayerTwoBoard
        {
            get { return _playerTwoBoard; }
        }

        public void Start()
        {
            //TODO
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Switch the current player with the other player. </summary>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Next turn add 1 turn. </summary>


        private void NextTurn()
        {
            _turn++;
            SwitchPlayer();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Play turn. </summary>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="coordinates">  The coordinates where the player is playing. </param>
        /// <param name="player">       (Optional) The player (needed only during warmup). </param>
        ///
        /// <returns>   An int. </returns>

        public int PlayTurn(Coordinates coordinates, Player player = null, bool isVertical = true)
        {
            if (player == null)
                player = _currentPlayer;

            try
            {


                if (_gameState.State == GameStates.States.Warmup)
                {
                    if (player.GetMissingBoat(_gameRules).Count != 0)
                    {
                        Ship shipToPlace = player.GetMissingBoat(_gameRules).First();
                        shipToPlace.StartPosition = coordinates;
                        if (isVertical == false)
                        {
                            shipToPlace.ChangeDirection();
                        }
                       
                        this.AddAShip(player, shipToPlace);
                    }


                    if (Player1.GetMissingBoat(_gameRules).Count == 0 && Player2.GetMissingBoat(_gameRules).Count == 0)
                    {
                        _gameState.State = GameStates.States.Playing;
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }

                }
                else if (_gameState.State == GameStates.States.Playing)
                {
                    if (player == _currentPlayer)
                    {
                        if (this.AddAShot(coordinates))
                        {
                            this.SwitchPlayer();
                            this.NextTurn();
                        }
                        else
                        {
                            return 10;
                        }
                    }
                    else
                    {
                        return 1;
                    }

                    if (this.IsOver())
                    {
                        _gameState.State = GameStates.States.End;
                        return 0;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else if (_gameState.State == GameStates.States.End)
                {
                    //TODO
                    return 0;
                }
                else
                {
                    new Exception("The Current Game State is not a possible state");
                    return 100;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a ship to a player. </summary>
        ///
        /// <param name="player">   The player who is placing the ship. </param>
        /// <param name="ship">     The ship. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a shot. </summary>
        ///
        /// <param name="coordinates">  The coordinates where the player is playing. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        private bool AddAShot(Coordinates coordinates)
        {
            GameRules gameRules = this.GameRules;
            if(_currentPlayer == _player1)
            {
                if(_currentPlayer.AddShot(coordinates,gameRules, _playerTwoBoard) == Hit.StatusType.notValid)
                {
                    return false;
                }
            }
            else if(_currentPlayer == _player2)
            {
                if(_currentPlayer.AddShot(coordinates,gameRules,_playerOneBoard) == Hit.StatusType.notValid)
                {
                    return false;
                }
            }
            else { return false; }
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Check if this game is over. </summary>
        ///
        /// <returns>   True if over, false if not. </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Return the game winner. </summary>
        ///
        /// <returns>   The winner. </returns>

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
