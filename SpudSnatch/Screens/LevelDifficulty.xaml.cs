﻿// LevelDifficulty.xaml.cs
// Contains the code for the difficulty selection screen

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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpudSnatch.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LevelDifficulty : Page
    {
        // Constructor
        public LevelDifficulty()
        {
            this.InitializeComponent();
        }

        // Button to select easy mode
        private void EasyButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Instance.SetDifficulty(Difficulty.Easy);
            Frame.Navigate(typeof(GamePage));
        }

        // Button to select medium mode
        private void MedButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Instance.SetDifficulty(Difficulty.Medium);
            Frame.Navigate(typeof(GamePage));
        }

        // Button to select hard mode
        private void HardButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Instance.SetDifficulty(Difficulty.Hard);
            Frame.Navigate(typeof(GamePage));
        }

        // Button to select death mode
        private void DeathButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Instance.SetDifficulty(Difficulty.Death);
            Frame.Navigate(typeof(GamePage));
        }
    }
}
