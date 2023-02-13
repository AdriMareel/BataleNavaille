using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace BatailleTest.Game.entity
{
    /// <summary>
    /// La classe <c>Hit</c> sert à stocker un tir effectué par un joueur.
    /// </summary>
    internal class Hit
    {
        protected utils.Coordinates _position;
        protected bool _isShipPiece;
        protected ShipPiece _shipPiece;

        public enum StatusType
        {
            Unknown,
            miss,
            hit,
            sunk,
            notValid
        }

        protected StatusType _status;

        protected int _score;

        /// <summary>
        /// Constructeur de la classe <c>Hit</c>
        /// </summary>
        /// <param name="position"><c>Coordinées</c> du tir sur le plateau de jeu.</param>
        /// <param name="status">Statut du tir (sunk, hit ou miss)</param>
        /// <param name="isShipPiece">Booléen indiquant si le tir touche une piece de bateau</param>
        /// <param name="shipPiece">Objet <c>ShipPiece</c> correspondant à la pièce touchée</param>
        public Hit(utils.Coordinates position, StatusType status,bool isShipPiece = false, ShipPiece shipPiece = null,int score = 0)
        {
            _position = position;
            _isShipPiece = isShipPiece;
            _shipPiece = shipPiece;
            _status = status;
            _score = score;
        }

        public utils.Coordinates Position
        {
            get { return _position; }
        }

        public bool IsShipPiece
        {
            get { return _isShipPiece; }
        }

        public ShipPiece ShipPiece
        {
            get { return _shipPiece; }
        }

        public StatusType Status
        {
            get { return _status; }
        }

        public int Score
        { 
            get { return _score; }
            set { _score = value; }
        }
           
        public void CalculHitScore(Board ennemyBoard)
        {

        }

    }
}
