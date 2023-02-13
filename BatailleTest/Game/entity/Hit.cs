using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    /// <summary>
    /// La classe <c>Hit</c> sert à stocker un tir effectué par un joueur.
    /// </summary>
    internal class Hit
    {
        private utils.Coordinates _position;
        private bool _isShipPiece;
        private ShipPiece _shipPiece;
        private string _status;

        /// <summary>
        /// Constructeur de la classe <c>Hit</c>
        /// </summary>
        /// <param name="position"><c>Coordinées</c> du tir sur le plateau de jeu.</param>
        /// <param name="status">Statut du tir (sunk, hit ou miss)</param>
        /// <param name="isShipPiece">Booléen indiquant si le tir touche une piece de bateau</param>
        /// <param name="shipPiece">Objet <c>ShipPiece</c> correspondant à la pièce touchée</param>
        public Hit(utils.Coordinates position, string status,bool isShipPiece = false, ShipPiece shipPiece = null)
        {
            _position = position;
            _isShipPiece = isShipPiece;
            _shipPiece = shipPiece;
            _status = status;
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

        public string Status
        {
            get { return _status; }
        }

    }
}
