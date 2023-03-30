using BatailleTest.DATA;
using BatailleTest.Game;
using BatailleTest.Game.entity;
using BatailleTest.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Display.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace BatailleTest
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private Game.Game game;
        private Game.GameRules gameRules;
        private Game.entity.Player player1;
        private Game.entity.Player player2;
        private Board boardPlayer1;
        private Board boardPlayer2;
        private Grid gridPlayer1;
        private Grid gridPlayer2;
        private Rectangle[,] gridElements = new Rectangle[10,10];
        private bool[,] botBoatsCoords = new bool[10, 10];
        private bool[,] playerBoatsCoords = new bool[10, 10];
        private bool vertical = true;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = e.Parameter;

            string namePlayer1 = ((List<string>)parameters)[0];
            string namePlayer2 = ((List<string>)parameters)[1];

            this.player1Name.Text = namePlayer1;
            this.player2Name.Text = namePlayer2;
        }
        
        public GamePage()
        {
            this.InitializeComponent();

            //init variables
            this.game = new Game.Game(player1Name : this.player1Name.Text, player2Name : this.player2Name.Text);
            this.gameRules = game.GameRules;
            this.player1 = game.Player1;
            this.player2 = game.Player2;
            this.boardPlayer1 = game.PlayerOneBoard;
            this.boardPlayer2 = game.PlayerTwoBoard;
            this.player2.RandomShips(gameRules);
            this.botBoatsCoords = getAIBoatsCoords(player2);
            this.gridPlayer1 = gamePlayer1;
            this.gridPlayer2 = gamePlayer2;

            initGridsView(this.gridPlayer1, this.gridPlayer2, this.botBoatsCoords);

            List<Ship> missingBoatsPlayer = player1.GetMissingBoat(gameRules);

            missingBoatsPlayer.ForEach(boat => 
                Debug.WriteLine(boat.Name)
            );

        }

        private bool[,] getAIBoatsCoords(Player player2)
        {
            const int GRID_SIZE = 10;
            bool[,] botBoatsCoords = new bool[GRID_SIZE, GRID_SIZE];

            foreach (var ship in player2.Ships)
            {
                foreach (ShipPiece shipPiece in ship.ShipPieces)
                {
                    botBoatsCoords[shipPiece.Position.X, shipPiece.Position.Y] = true;
                }
            }
            return botBoatsCoords;
        }

        private void initGridsView(Grid gridPlayer1, Grid gridPlayer2, bool[,] botBoatsCoords)
        {
            const int GRID_SIZE = 10;
            //créations des grilles pour l'interface en fonction de la GRID_SIZE choisie pour le joueur 
            for (int i = 0; i < GRID_SIZE; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(1, GridUnitType.Star);
                gridPlayer1.ColumnDefinitions.Add(c);

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(1, GridUnitType.Star);
                gridPlayer1.RowDefinitions.Add(r);
            }

            for (int i = 0; i < GRID_SIZE; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(1, GridUnitType.Star);
                gridPlayer2.ColumnDefinitions.Add(c);

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(1, GridUnitType.Star);
                gridPlayer2.RowDefinitions.Add(r);
            }

            for (int a = 0; a < GRID_SIZE; a++)
            {
                for (int b = 0; b < GRID_SIZE; b++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.SetValue(Grid.ColumnProperty, b);
                    rectangle.SetValue(Grid.RowProperty, a);
                    rectangle.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                    gridPlayer1.Children.Add(rectangle);
                    this.gridElements[a, b] = rectangle;

                    Rectangle rectangle2 = new Rectangle();
                    rectangle2.SetValue(Grid.ColumnProperty, b);
                    rectangle2.SetValue(Grid.RowProperty, a);
                    rectangle2.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                    gridPlayer2.Children.Add(rectangle2);

                    //affichage des coords des bateaux de l'IA
                    if (botBoatsCoords[a, b])
                    {
                        rectangle2.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    }

                    //set event listeners
                    rectangle.PointerEntered += Grid_PointerEntered;
                    rectangle.Tapped += Grid_Tapped;
                    rectangle.RightTapped += Rectangle_RightTapped;
                }
            }
        }

        private void Rectangle_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            this.vertical = !this.vertical;

            Rectangle rectangle = sender as Rectangle;
            rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            // Obtenir les coordonnées de la case survolée
            int x = Grid.GetColumn(rectangle);
            int y = Grid.GetRow(rectangle);


            this.generatePreview(x, y);
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            // Obtenir les coordonnées de la case survolée
            int x = Grid.GetColumn(rectangle);
            int y = Grid.GetRow(rectangle);


            Coordinates coord = new Coordinates(x, y);
            this.game.PlayTurn(coord, this.player1, this.vertical);

            this.refreshBoatView();

        }
        
        private void refreshBoatView()
        {
            foreach (Ship ship in this.boardPlayer1.PlayerShips)
            {
                foreach (ShipPiece piece in ship.ShipPieces)
                {
                    Rectangle rectangleFollowing = this.gridElements[piece.Position.Y, piece.Position.X];
                    rectangleFollowing.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                    this.playerBoatsCoords[piece.Position.Y, piece.Position.X] = true;
                }
            }
        }


        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            // Obtenir les coordonnées de la case survolée
            int x = Grid.GetColumn(rectangle);
            int y = Grid.GetRow(rectangle);


            this.generatePreview(x, y);
        }

        private void generatePreview(int x, int y)
        {
            removePreview();
            // Obtenir la liste des bateaux manquants pour le joueur 1
            List<Ship> missingBoatsPlayer = this.player1.GetMissingBoat(gameRules);
            Coordinates coord = new Coordinates(x, y);

            //if missing boat is not empty
            if (missingBoatsPlayer.Count < 1)
            {
                return;
            }
            var boat = missingBoatsPlayer[0];
            boat.StartPosition = coord;
            
            if (!this.vertical)
            {
                boat.ChangeDirection();
            }

            foreach (ShipPiece piece in boat.ShipPieces)
            {
                if(this.player1.doesShipFit(boat, this.game.GameRules))
                {
                    Rectangle rectangleFollowing = this.gridElements[piece.Position.Y,piece.Position.X];
                    rectangleFollowing.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                }
            }
        }

        private void removePreview()
        {
            foreach (Rectangle rectangle in this.gridPlayer1.Children)
            {
                rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
                int x = Grid.GetColumn(rectangle);
                int y = Grid.GetRow(rectangle);
                if (this.playerBoatsCoords[y, x])
                {
                    rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.Blue); 
                }
            }
        }

        private void randomBtn_Click(object sender, RoutedEventArgs e)
        {
            this.game.CurrentPlayer.RandomShips(this.game.GameRules);
            this.refreshBoatView();
        }
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
