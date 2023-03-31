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
using Windows.UI.Notifications;
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
        private Grid gridBoatPlayer1;
        private Grid gridHitPlayer1;
        private Rectangle[,] gridBoatElements = new Rectangle[10,10];
        private Rectangle[,] gridHitElements = new Rectangle[10, 10];
        private bool[,] botBoatsCoords = new bool[10, 10];
        private bool[,] playerBoatsCoords = new bool[10, 10];
        private bool vertical = true;
        public string GameState = "Boat placing !";

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
            this.gridBoatPlayer1 = gamePlayer1;
            this.gridHitPlayer1 = gamePlayer2;


            initGridsView(this.gridBoatPlayer1, this.gridHitPlayer1, this.botBoatsCoords);

            List<Ship> missingBoatsPlayer = player1.GetMissingBoat(gameRules);

            missingBoatsPlayer.ForEach(boat => 
                Debug.WriteLine(boat.Name)
            );

        }

        private bool[,] getAIBoatsCoords(Player player2)
        {
            int GRID_SIZE = this.game.GameRules.MapSize;
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

        private void initGridsView(Grid gridBoatPlayer1, Grid gridHitPlayer1, bool[,] botBoatsCoords)
        {
            int GRID_SIZE = this.game.GameRules.MapSize;
            //créations des grilles pour l'interface en fonction de la GRID_SIZE choisie pour le joueur 
            for (int i = 0; i < GRID_SIZE; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(1, GridUnitType.Star);
                gridBoatPlayer1.ColumnDefinitions.Add(c);

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(1, GridUnitType.Star);
                gridBoatPlayer1.RowDefinitions.Add(r);
            }

            for (int i = 0; i < GRID_SIZE; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(1, GridUnitType.Star);
                gridHitPlayer1.ColumnDefinitions.Add(c);

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(1, GridUnitType.Star);
                gridHitPlayer1.RowDefinitions.Add(r);
            }

            for (int a = 0; a < GRID_SIZE; a++)
            {
                for (int b = 0; b < GRID_SIZE; b++)
                {
                    Rectangle rectangleBoat = new Rectangle();
                    rectangleBoat.SetValue(Grid.ColumnProperty, b);
                    rectangleBoat.SetValue(Grid.RowProperty, a);
                    rectangleBoat.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                    rectangleBoat.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                    gridBoatPlayer1.Children.Add(rectangleBoat);
                    this.gridBoatElements[a, b] = rectangleBoat;

                    Rectangle rectangleHit = new Rectangle();
                    rectangleHit.SetValue(Grid.ColumnProperty, b);
                    rectangleHit.SetValue(Grid.RowProperty, a);
                    rectangleHit.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                    rectangleHit.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                    gridHitPlayer1.Children.Add(rectangleHit);
                    this.gridHitElements[a, b] = rectangleHit;

                    //set event listeners
                    rectangleBoat.PointerEntered += GridBoat_PointerEntered;
                    rectangleBoat.Tapped += GridBoat_Tapped;
                    rectangleBoat.RightTapped += RectangleBoat_RightTapped;

                    rectangleHit.PointerEntered += GridHit_PointerEntered;
                    rectangleHit.Tapped += GridHit_Tapped;
                    rectangleHit.RightTapped += RectangleHit_RightTapped;
                    this.gridHitPlayer1.PointerExited += GridHit_Exited;
                }
            }
        }

        private void GridHit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!this.game.isGameStarted())
            {
                return;
            }
            Rectangle rectangle = sender as Rectangle;

            // Obtenir les coordonnées de la case survolée
            int x = Grid.GetColumn(rectangle);
            int y = Grid.GetRow(rectangle);


            Coordinates coord = new Coordinates(x, y);
            this.game.PlayTurn(coord, this.player1, this.vertical);

            this.refreshPlayerHitView();
            this.refreshBoatView();
        }

        private void RectangleHit_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            return;
        }

        private void GridHit_PointerEntered(object sender, PointerRoutedEventArgs e)
        {    
            if(!this.game.isGameStarted() && this.game.CurrentPlayer == this.game.Player1)
            {
                return;
            }

            this.refreshPlayerHitView();
                
            Rectangle rectangle = sender as Rectangle;
            rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.DarkCyan);
        }

        private void RectangleBoat_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            this.vertical = !this.vertical;

            Rectangle rectangle = sender as Rectangle;
            rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            rectangle.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
            // Obtenir les coordonnées de la case survolée
            int x = Grid.GetColumn(rectangle);
            int y = Grid.GetRow(rectangle);


            this.generatePreview(x, y);
        }

        private void GridBoat_Tapped(object sender, TappedRoutedEventArgs e)
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
            int GRID_SIZE = this.game.GameRules.MapSize;

            foreach (Rectangle rec in this.gridBoatElements)
            {
                rec.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                rec.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
            }

            for (int a = 0; a < GRID_SIZE; a++)
            {
                for (int b = 0; b < GRID_SIZE; b++)
                {
                    this.playerBoatsCoords[a, b] = false;
                }
            }

            //affichage bateau joueur 1
            foreach (Ship ship in this.boardPlayer1.PlayerShips)
            {
                foreach (ShipPiece piece in ship.ShipPieces)
                {
                    Rectangle rectangleFollowing = this.gridBoatElements[piece.Position.Y, piece.Position.X];
                    rectangleFollowing.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                    rectangleFollowing.Stroke = new SolidColorBrush(Windows.UI.Colors.Orange);
                    this.playerBoatsCoords[piece.Position.Y, piece.Position.X] = true;
                }
            }

            //affichage hits joueur1
            foreach (Hit hit in this.player2.PlayerShots)
            {
                Rectangle rectangle = this.gridBoatElements[hit.Position.Y, hit.Position.X];

                SolidColorBrush color = new SolidColorBrush(Windows.UI.Colors.White);

                if (hit.Status == Hit.StatusType.miss)
                {
                    color = new SolidColorBrush(Windows.UI.Colors.BlueViolet);
                }
                else
                if (hit.Status == Hit.StatusType.hit)
                {
                    color = new SolidColorBrush(Windows.UI.Colors.DarkRed);
                }
                else
                if (hit.Status == Hit.StatusType.sunk)
                {
                    color = new SolidColorBrush(Windows.UI.Colors.IndianRed);
                }
                rectangle.Fill = color;
                rectangle.Stroke = color;
            }
        }

        private void refreshPlayerHitView()
        {
            foreach (Rectangle rec in this.gridHitElements)
            {
                rec.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                rec.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
            }
            foreach(Hit hit in this.boardPlayer1.PlayerShots)
            {

                Rectangle rectangle = this.gridHitElements[hit.Position.Y, hit.Position.X];

                SolidColorBrush color = new SolidColorBrush(Windows.UI.Colors.White);

                if(hit.Status == Hit.StatusType.miss)
                {
                    color = new SolidColorBrush(Windows.UI.Colors.BlueViolet);
                }
                else 
                if(hit.Status == Hit.StatusType.hit)
                {
                    color = new SolidColorBrush(Windows.UI.Colors.DarkRed);
                }else
                if(hit.Status == Hit.StatusType.sunk)
                {
                    color = new SolidColorBrush(Windows.UI.Colors.IndianRed);
                }

                rectangle.Fill = color;
                rectangle.Stroke = color;

            }
        }


        private void GridBoat_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
            rectangle.Stroke = new SolidColorBrush(Windows.UI.Colors.Orange);
            // Obtenir les coordonnées de la case survolée
            int x = Grid.GetColumn(rectangle);
            int y = Grid.GetRow(rectangle);


            this.generatePreview(x, y);
        }

        private void generatePreview(int x, int y)
        {
            if (this.game.isGameStarted())
            {
                return;
            }

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
                    Rectangle rectangleFollowing = this.gridBoatElements[piece.Position.Y,piece.Position.X];
                    rectangleFollowing.Fill = new SolidColorBrush(Windows.UI.Colors.LightCyan);
                    rectangleFollowing.Stroke = new SolidColorBrush(Windows.UI.Colors.LightCyan);
                }
            }
        }

        private void removePreview()
        {
            foreach (Rectangle rectangle in this.gridBoatPlayer1.Children)
            {
                rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                rectangle.Stroke = new SolidColorBrush(Windows.UI.Colors.White);

                int x = Grid.GetColumn(rectangle);
                int y = Grid.GetRow(rectangle);
                if (this.playerBoatsCoords[y, x])
                {
                    rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                    rectangle.Stroke = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
            }
        }
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            this.game.CurrentPlayer.ClearAllShips();
            this.refreshBoatView();
        }
        private void randomBtn_Click(object sender, RoutedEventArgs e)
        {
            this.game.CurrentPlayer.RandomShips(this.game.GameRules);
            this.refreshBoatView();
        }
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.game.CurrentPlayer.GetMissingBoat(this.game.GameRules).Count() == 0)
            {
                //do not display these buttons anymore
                this.startBtn.Visibility = Visibility.Collapsed;
                this.randomBtn.Visibility = Visibility.Collapsed;
                this.clearBtn.Visibility = Visibility.Collapsed;
            
                this.game.Start();
                this.gameStatus.Text = "Your turn !";
            }
            else
            {
                this.gameStatus.Text = "You have not placed all your boats ! The game can't start !";
            }
        }

        private void GridHit_Exited(object sender, PointerRoutedEventArgs e)
        {
            this.refreshPlayerHitView();
        }
    }
}
