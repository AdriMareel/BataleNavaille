using BatailleTest.Game;
using BatailleTest.Game.entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace BatailleTest
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
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

            Game.Game game = new Game.Game(player1Name : this.player1Name.Text, player2Name : this.player2Name.Text);
            Game.GameRules gameRules = game.GameRules;
            Game.entity.Player player1 = game.Player1;
            Game.entity.Player player2 = game.Player2;
            Board boardPlayer1 = game.PlayerOneBoard;
            Board boardPlayer2 = game.PlayerTwoBoard;


            player2.RandomShips(gameRules);

            TextBlock tb = new TextBlock();

            foreach (var ship in player2.Ships)
            {
                foreach (ShipPiece shipPiece in ship.ShipPieces)
                {
                tb.Text += shipPiece.Position.X.ToString();
                tb.Text += " ";
                tb.Text += shipPiece.Position.Y.ToString();
                tb.Text += " - ";   
                }
                tb.Text += "___";
            }
            console.Child = tb;
            Debug.WriteLine(player2.Ships.Count);
            
            

            const int GRID_SIZE = 10;
            Grid gridPlayer1 = gamePlayer1;
            Grid gridPlayer2 = gamePlayer2;

            //créations des grilles pour l'interface en fonction de la GRID_SIZE choisie pour le joueur 
            for (int i = 0; i < GRID_SIZE; i++){
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(50, GridUnitType.Pixel);
                gridPlayer1.ColumnDefinitions.Add(c);

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(50, GridUnitType.Pixel);
                gridPlayer1.RowDefinitions.Add(r);
            }

            for (int i = 0; i < GRID_SIZE; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(50, GridUnitType.Pixel);
                gridPlayer2.ColumnDefinitions.Add(c);

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(50, GridUnitType.Pixel);
                gridPlayer2.RowDefinitions.Add(r);
            }
            
            for (int a = 0; a < GRID_SIZE; a++)
            {
                for (int b = 0; b < GRID_SIZE; b++)
                {
                    //ajout des borders aux grids 
                    Border border = new Border();
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
                    border.BorderThickness = new Thickness(1);
                    border.SetValue(Grid.ColumnProperty, b);
                    border.SetValue(Grid.RowProperty, a);
                    gridPlayer1.Children.Add(border);

                    Border border2 = new Border();
                    border2.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
                    border2.BorderThickness = new Thickness(1);
                    border2.SetValue(Grid.ColumnProperty, b);
                    border2.SetValue(Grid.RowProperty, a);
                    gridPlayer2.Children.Add(border2);

                    border.PointerEntered += Grid_PointerEntered;
                    border.PointerExited += Grid_PointerExited;
                }
            }

            
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Border border = sender as Border;
            border.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Border border = sender as Border;
            border.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
