﻿// EndScreen.xaml.cs
// Contains the code for the end screen

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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpudSnatch.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EndScreen : Page
    {
        // variable to hold the high score object
        HighScoreVariables passmoreparams = new HighScoreVariables();

        // Constructor
        public EndScreen()
        {
            this.InitializeComponent();
        }

        // Method called when being navigated to.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            passmoreparams= (HighScoreVariables)e.Parameter;

            Score.Text = "Score: " + passmoreparams.Score;
            Time.Text = "Time: " + passmoreparams.Time;
            
        }
        
        // Button to go to the high score screen.
        private void checkHighScore_Click(object sender, RoutedEventArgs e)
        {
            passmoreparams.Name = PlayerName.Text;
            Frame.Navigate(typeof(HighScore), passmoreparams);
        }
    }
}
