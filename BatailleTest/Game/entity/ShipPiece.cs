using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    /// <summary>
    /// Classe définissant une pièce appartenant à un bateau <c>Ship</c>
    /// </summary>
    internal class ShipPiece
    {
        private utils.Coordinates _position;
        private bool _isHit;
        
        /// <summary>
        /// Constructeur de classe
        /// </summary>
        /// <param name="position">Position de la pièce</param>
        public ShipPiece(utils.Coordinates position)
        {
            _position = position;
            _isHit = false;
        }

        public bool IsHit
        {
            get { return _isHit; }
            set { _isHit = value; }
        }

        public utils.Coordinates Position
        {
            get { return _position; }

        }
    }
}
