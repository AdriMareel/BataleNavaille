using System;
using System.Collections.Generic;
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


            const int GRID_SIZE = 10;
            Grid gridPlayer1 = gamePlayer1;
            Grid gridPlayer2 = gamePlayer2;

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

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for(int y = 0; y < GRID_SIZE; y++) {
                    Border border = new Border();
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
                    border.BorderThickness = new Thickness(1);
                    border.SetValue(Grid.ColumnProperty, i);
                    border.SetValue(Grid.RowProperty, y);
                    gridPlayer1.Children.Add(border);

                    Border border2 = new Border();
                    border2.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
                    border2.BorderThickness = new Thickness(1);
                    border2.SetValue(Grid.ColumnProperty, i);
                    border2.SetValue(Grid.RowProperty, y);
                    gridPlayer2.Children.Add(border2);


                }
            }

            gridPlayer2.Margin = new Thickness(1000, 0, 0, 0);
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
