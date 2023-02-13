using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleTest.Game.entity
{
    /// <summary>
    /// Classe représentant un bateau 
    /// </summary>
    internal class Ship
    {
        private int _size;
        private string _name;
        private bool _isVertical;
        private utils.Coordinates _startPosition;
        private List<ShipPiece> _shipPieces;

        private int _life;
        private bool _isAlive;

        /// <summary>
        /// Constructeur de la classe <c>Ship</c>
        /// </summary>
        /// <param name="startPosition">Position de la première pièce du bateau</param>
        /// <param name="name">Nom du bateau</param>
        /// <param name="size">Taille du bateau</param>
        /// <param name="isVertical">Est=ce que le bateau est orienté verticalement ou non</param>
        public Ship(utils.Coordinates startPosition,string name , int size, bool isVertical = true)
        {
            _startPosition = startPosition;
            _name = name;
            _size = size;
            _life = size;
            _isVertical = isVertical;
            _isAlive = true;

            generateBoatPieces();
        }

        /// <summary>
        /// Génère les pièces individuelles qui composent le bateau
        /// </summary>
        private void generateBoatPieces()
        {
            _shipPieces = new List<ShipPiece>();
            for (int i = 0; i < _size; i++)
            {
                if (_isVertical)
                {
                    _shipPieces.Add(new ShipPiece(new utils.Coordinates(_startPosition.X, _startPosition.Y + i)));
                }
                else
                {
                    _shipPieces.Add(new ShipPiece(new utils.Coordinates(_startPosition.X + i, _startPosition.Y)));
                }
            }
        }

        public string Name
        {
            get { return _name; }
        }
        public bool IsAlive
        {
            get { return _isAlive; }
        }

        public int Life
        {
            get { return _life; }
        }

        public bool IsVertical
        {
            get { return _isVertical; }
            set
            {
                _isVertical = value;
                generateBoatPieces();
            }
        }

        public int Size
        {
            get { return _size; }
        }

        public utils.Coordinates StartPosition
        {
            get { return _startPosition; }
            set 
            { 
                _startPosition = value;
                this.generateBoatPieces();
            }
        }

        public List<ShipPiece> ShipPieces
        {
            get { return _shipPieces; }
        }

        /// <summary>
        /// Déclare que le bateau a été touchée
        /// </summary>
        /// <param name="position">Coordonnées touchée</param>
        public void Hit(utils.Coordinates position)
        {
            foreach (ShipPiece boatPiece in _shipPieces)
            {
                if (boatPiece.Position.X == position.X && boatPiece.Position.Y == position.Y)
                {
                    boatPiece.IsHit = true;
                    _life--;
                    if (_life == 0)
                    {
                        _isAlive = false;
                    }
                }
            }
        }
        /// <summary>
        /// Test si un bateau est égal à un autre
        /// </summary>
        /// <param name="ship1">Premier bateau</param>
        /// <param name="ship2">Deuxième bateau</param>
        /// <returns>Vrai si les deux bateaux sont les mêmes, faux sinon</returns>
        public static bool operator ==(Ship ship1, Ship ship2)
        {
            if (ship1.Size == ship2.Size && ship1.Name == ship2.Name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Ship ship1, Ship ship2)
        {
            return !(ship1 == ship2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Ship ship = obj as Ship;
            if ((System.Object)ship == null)
            {
                return false;
            }

            return (this == ship);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string result = "Ship " + _name + " of size " + _size + " at position " + _startPosition.ToString();
            return result;
        }

    }
}
