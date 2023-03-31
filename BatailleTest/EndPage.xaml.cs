using BatailleTest.Game.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BatailleTest
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class EndPage : Page
    {
        Game.Game game;
        public EndPage()
        {
            this.InitializeComponent();
            displayEndGame();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = e.Parameter;

             game = (Game.Game)parameters;
            
        }

        public void displayEndGame()
        {
            Player winner = this.game.GetWinner();
            if (winner == game.Player1)
            {
                winnerText.Text = "Vous avez gagné !";
                //MediaPlayerElement.MediaPlayer.Source = new Uri("ms-appx:///Assets/victory.mp3");

                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/myimage.png"));
                GifGrid.Background = brush;
            }
            else if (winner == game.Player2)
            {
                winnerText.Text = "Vous avez perdu !";
            }
            
        }
    }
}
