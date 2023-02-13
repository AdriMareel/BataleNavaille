using BatailleTest.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace BatailleTest.Game.entity
{
    /// <summary>
    /// Classe représentant un joueur
    /// </summary>
    internal class Player
    {
        private string _name;
        private int _score;
        private List<Ship> _ships;

        private List<Hit> _playerShots;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="name">Nom du joueur</param>
        public Player(string name)
        {
            _name = name;
            _score = 0;
            _ships = new List<Ship>();
            _playerShots = new List<Hit>();
        }

        public string Name
        {
            get { return _name; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public List<Ship> Ships
        {
            get { return _ships; }
        }

        /// <returns>Le nombre de bateau d'un joueur</returns>
        public int getNumberOfShip()
        {
            return _ships.Count;
        }

        public List<Hit> PlayerShots
        {
            get { return _playerShots; }
        }

        /// <summary>
        /// Verifie si une position contient une partie d'un bateau
        /// </summary>
        /// <param name="position">Coordonnée à verifier</param>
        /// <returns>Un booléen vrai si un bateau est à la position choisie, faux sinon</returns>
        public bool isAShipAt(utils.Coordinates position)
        {
            foreach (Ship ship in _ships)
            {
                foreach (ShipPiece boatPiece in ship.ShipPieces)
                {
                    if (boatPiece.Position.X == position.X && boatPiece.Position.Y == position.Y)
                    {
                        Debug.WriteLine("The ship " + ship.Name + " is there");
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Renseigne quel bateau est à une position
        /// </summary>
        /// <param name="position">Coordonnée à vérifier</param>
        /// <returns>L'index du bateau ou -1 si il n'y a pas de bateau</returns>
        public int getIndexOfShipAt(utils.Coordinates position)
        {
            for (int i = 0; i < _ships.Count; i++)
            {
                foreach (ShipPiece boatPiece in _ships[i].ShipPieces)
                {
                    if (boatPiece.Position.X == position.X && boatPiece.Position.Y == position.Y)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Est-ce qu'un bateau rentre aux coordonnées qu'on lui a défini
        /// </summary>
        /// <param name="ship">Le bateau <c>Ship</c> a tester</param>
        /// <param name="rules">La règle du jeu actuelle</param>
        /// <returns>Vrai si le bateau peut être placé, faux sinon</returns>
        public bool doesShipFit(Ship ship, GameRules rules)
        {
            foreach (ShipPiece shipPiece in ship.ShipPieces)
            {
                var boardSize = rules.MapSize;
                if (shipPiece.Position.X > boardSize || shipPiece.Position.Y > boardSize || shipPiece.Position.X < 0 || shipPiece.Position.Y < 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Est=ce qu'il reste un bateau en vie sur le plateau du joueur actuel
        /// </summary>
        /// <returns>Vrai si il reste au moins un bateau, faux sinon</returns>
        public bool isAShipAlive()
        {
            foreach (Ship ship in _ships)
            {
                if (ship.IsAlive)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permet d'ajouter un bateau à la liste des bateaux du joueur 
        /// </summary>
        /// <param name="ship">Bateau à ajouter</param>
        /// <param name="rules">Règle de la partie actuelle</param>
        /// <returns>Vrai si le bateau à été placé et ajouté à la liste, faux sinon</returns>
        public bool AddShip(Ship ship, GameRules rules)
        {

            if(_ships.Count == rules.MaxShip)
            {
                Debug.WriteLine("There is already the good number of ship");
                return false;
            }
            
            foreach (ShipPiece shipPiece in ship.ShipPieces)
            {
                if (isAShipAt(shipPiece.Position))
                {
                    Debug.WriteLine("There is already a ship there");
                    return false;
                }
            }
            if (!doesShipFit(ship, rules))
            {
                Debug.WriteLine("The ship does not fit");
                return false;
            }
            
            Debug.WriteLine("Add the ship " + ship.Name + " at " + ship.StartPosition.X + " " + ship.StartPosition.Y);
            _ships.Add(ship);
            return true;
        }

        /// <summary>
        /// Permet de savoir si il reste des bateaux à placer sur le plateau
        /// </summary>
        /// <param name="rules">Règles de la partie actuelle</param>
        /// <returns>Une liste de bateaux pas encore placés</returns>
        public List<Ship> GetMissingBoat(GameRules rules)
        {
            List<Ship> missingBoat = new List<Ship>();

            foreach(string shipName in rules.ShipList.Keys) 
            {
                bool isTheBoatMissing = true;

                foreach(Ship playerShip in _ships)
                {
                    if(playerShip.Name == shipName)
                    {
                        isTheBoatMissing = false;
                        break;
                    }
                }
                if (isTheBoatMissing)
                {
                    missingBoat.Add(new Ship(startPosition: new Coordinates(0, 0), name: shipName, size: rules.ShipList[shipName]));
                }
            }

            Debug.WriteLine("Missing boat : " + missingBoat.Count + " for player: " + this.Name);
            return missingBoat;
        }

        /// <summary>
        /// Place aléatoirement tous les bateaux du joueur (prend en compte le fait qu'il en ai déjà placé certains)
        /// </summary>
        /// <param name="rules">Règles de la partie actuelle</param>
        /// <returns>"ok" si tout s'est bien déroulé, "error" sinon</returns>
        public string RandomShips(GameRules rules)
        {
            List<Ship> missing = GetMissingBoat(rules);
            Random r = new Random();

            const int SAFETY_LIMIT = 1000;
            int iterator = 0;
            
            foreach (Ship ship in missing)
            {
                Coordinates randCoords = new Coordinates();
                bool randDir = false;
                do
                {
                    randCoords.Randomize(0, rules.MapSize-1);
                    ship.StartPosition = randCoords;
                    
                    randDir = r.NextDouble() >= 0.5;
                    ship.IsVertical = randDir;

                    if (++iterator > SAFETY_LIMIT)
                    {
                        new Exception("Safety limit reached");
                        Debug.WriteLine("Safety limit reached");
                        return "error";
                    }
                } while (!AddShip(ship, rules));
            }
            return "ok";
        }

        /// <summary>
        /// Permet de savoir si un tir a déjà été effectué à une certaine position
        /// </summary>
        /// <param name="position">Coordonnées de la position à vérifier</param>
        /// <returns>Vrai si un tir a déjà été effectué à la position choisie, faux sinon</returns>
        public bool isAShotAt(utils.Coordinates position)
        {
            foreach (Hit hit in _playerShots)
            {
                if (hit.Position.X == position.X && hit.Position.Y == position.Y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ajoute un tir sur le plateau de l'ennemi
        /// </summary>
        /// <param name="coords">Coordonnéesdu tir à effectuer</param>
        /// <param name="rules">Règles de la partie actuelle</param>
        /// <param name="ennemyBoard">Plateau de  jeu ennemi</param>
        /// <returns>"notValid" si le tir est impossible, "miss" si tir raté, "hit" ou "sunk" si bateau ennemi touché</returns>
        public string AddShot(Coordinates coords, GameRules rules, Board ennemyBoard)
        {

            //check if the number of shot is not greater than the number of shot allowed (map size)²
            if(_playerShots.Count >= (rules.MapSize^2))
            {
                return "notValid";
            }
            //check if the shot is not out of the board
            if(coords.X < 0 || coords.Y < 0 || coords.X >= rules.MapSize || coords.Y >= rules.MapSize)
            {
                return "notValid";
            }
            //check if the shot is not already done
            foreach(Hit hitTmp in _playerShots)
            {
                if(hitTmp.Position == coords)
                {
                    return "notValid";
                }
            }

            //check if shot is landed or not and if it sunk a full ship
            var status = "";
            foreach (Ship ship in ennemyBoard.PlayerShips)
            {
                foreach(ShipPiece piece in ship.ShipPieces)
                {
                    if(coords == piece.Position)
                    {
                        if(ship.Life == 1)
                        {
                            status = "sunk";
                        }
                        else
                        {
                            status = "hit";
                        }
                        ship.Hit(coords);
                    }
                    
                }
            }
            if (status == "") { status = "miss"; }

            Hit hit = new Hit(coords, status);
            _playerShots.Add(hit);

            return status;
        }

    }
}
