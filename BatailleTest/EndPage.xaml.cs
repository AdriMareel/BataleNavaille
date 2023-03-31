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
using System.Diagnostics;

namespace BatailleTest
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class EndPage : Page
    {
        Game.Game game;
        bool isWon;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = e.Parameter;

            bool isWon = (bool)parameters;
        }

        public EndPage()
        {
            this.InitializeComponent();
            displayEndGame();
        }

        public void displayEndGame()
        {
            if (this.isWon)
            {
                winnerText.Text = "Vous avez gagné !";
                //MediaPlayerElement.MediaPlayer.Source = new Uri("ms-appx:///Assets/victory.mp3");

                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/victory.gif"));
                GifGrid.Background = brush;
            }
            else
            {
                winnerText.Text = "Vous avez perdu !";
            }
            
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }
    }
}
