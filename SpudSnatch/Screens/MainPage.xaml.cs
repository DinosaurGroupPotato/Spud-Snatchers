// MainPage.xaml.cs
// Contains the code for the menu screen

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
using SpudSnatch.Model;
using SpudSnatch.Screens;
using SpudSnatch.Model.Serialization;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpudSnatch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Instance of the game controller
        static GameController game = GameController.Instance;

        // Constructor
        public MainPage()
        {
            this.InitializeComponent();
        }

        // Button to start the game
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LevelDifficulty));
        }

        // Button to go to the about screen
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        // Button to get help
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help));
        }

        // Button to get to the high score screen
        private void HighScoreButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HighScore));
        }

        // Button to load a saved game
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
            SerializeData.DeserializeInfo("SaveData");
        }
    }
}
