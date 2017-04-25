// HighScore.xaml.cs
// Contains the code for the high score screen

﻿using System;
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
using SpudSnatch.Model.HighScores;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpudSnatch.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScore : Page
    {
        // holds the high score entry
        HighScoreVariables finalPass = new HighScoreVariables();

        // Constructor
        public HighScore()
        {
            this.InitializeComponent();
        }

        // Method called when the page is loaded, gets the new highscore.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            finalPass = (HighScoreVariables)e.Parameter;

            try
            {
                HighScoreList.Instance.AddEntry(finalPass.Name, Convert.ToInt32(finalPass.Score), finalPass.Time);

                foreach (HighScoreEntry entry in HighScoreList.Instance.Scores)
                {
                    score.Text += "Player: " + entry.GiveName() + "                                        " + entry.GiveScore() + "                                  " + entry.GiveTime() + "\n";
                }
            }
            catch
            {
                score.Text = "No one's played yet.";
            }
        }

        // Method to return to the menu page
        private void returnToMainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}