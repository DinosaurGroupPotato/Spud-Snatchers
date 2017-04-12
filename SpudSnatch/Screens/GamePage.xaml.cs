﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpudSnatch.Model;
using SpudSnatch.Model.Objects;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;
using SpudSnatch.State;

namespace SpudSnatch
{

    public sealed partial class GamePage : Page
    {
        
        // Timer to keep track of updates
        DispatcherTimer timer;

        // Labels for score and time
        TextBlock ScoreLabel;
        TextBlock TimeLabel;

        // Lists of image objects
        List<Image> Potatoes, Obstacles, Enemies;
        Image Homer;

        public GamePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;
            timer.Start();

            GameController.level.player.HomerUpdated += UpdateHomer;
            
            SetUpImages();
            KeyboardState.InitializeKeys();
        }

        private void SetUpImages()
        {
            // Add potatoes
            Potatoes = new List<Image>();
            foreach (Potato potato in GameController.level.GetPotatoes())
            {
                Image PotatoImage = new Image();
                PotatoImage.Margin = new Windows.UI.Xaml.Thickness(potato.positionX, potato.positionY, 0, 0);
                PotatoImage.Width = 20;
                PotatoImage.Height = 20;
                PotatoImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Potato/Potato.png"));
                Potatoes.Add(PotatoImage);
                GameGrid.Children.Add(PotatoImage);
            }
            // Add enemies
            Enemies = new List<Image>();
            foreach (Enemy enemy in GameController.level.GetEnemies())
            {
                Image EnemyImage = new Image();
                EnemyImage.Margin = new Windows.UI.Xaml.Thickness(enemy.positionX, enemy.positionY, 0, 0);
                EnemyImage.Width = 50;
                EnemyImage.Height = 50;
                EnemyImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Enemy/StaticImages/Enemy_standing.png"));
                Enemies.Add(EnemyImage);
                GameGrid.Children.Add(EnemyImage);
            }
            // Add Obstacles
            Obstacles = new List<Image>();
            foreach (Obstacle obstacle in GameController.level.GetObstacles())
            {
                Image obstacleImage = new Image();
                obstacleImage.Margin = new Windows.UI.Xaml.Thickness(obstacle.positionX, obstacle.positionY, 0, 0);
                obstacleImage.Width = 150;
                obstacleImage.Height = 100;
                obstacleImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Platform/Platform.png"));
                Obstacles.Add(obstacleImage);
                GameGrid.Children.Add(obstacleImage);
            }
            // Add Homer
            Homer = new Image();
            Homer.Margin = new Windows.UI.Xaml.Thickness(GameController.level.ReturnPlayerPosition("x", GameController.level.GetHomer()), GameController.level.ReturnPlayerPosition("y", GameController.level.GetHomer()), 0, 0);
            Homer.Width = 50;
            Homer.Height = 50;
            Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/stand.jpg"));
            GameGrid.Children.Add(Homer);
        }

        private void Timer_Tick(object sender, object e)
        {
            // Update objects (using KeyBoardState)

            GameController.level.player.Update();
            GameController.UpdateGameController();

            //TimeLabel.Text = GameController.Time.ToString();
            //ScoreLabel.Text = GameController.Score.ToString();
        }

        private void UpdatePotatoes(int id)
        {

        }

        private void UpdateHomer(object sender, EventArgs e)
        {
            Homer.Margin = new Windows.UI.Xaml.Thickness(GameController.level.ReturnPlayerPosition("x", GameController.level.GetHomer()), GameController.level.ReturnPlayerPosition("y", GameController.level.GetHomer()), 0, 0);
        }

        private void UpdateObjects(int id)
        {

        }

        // Handles the key-up event and sets the values in the keyboard state accordingly
        private void Grid_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.W) { KeyboardState.W = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.A) { KeyboardState.A = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.S) { KeyboardState.S = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.D) { KeyboardState.D = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.Up) { KeyboardState.Up = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.Left) { KeyboardState.Left = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.Down) { KeyboardState.Down = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.Right) { KeyboardState.Right = KeyState.Down; }
            if (e.Key == Windows.System.VirtualKey.Space) { KeyboardState.Space = KeyState.Down; }
        }

        // Handles the key-down event and sets the values in the keyboard state accordingly
        private void Grid_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.W) { KeyboardState.W = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.A) { KeyboardState.A = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.S) { KeyboardState.S = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.D) { KeyboardState.D = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.Up) { KeyboardState.Up = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.Left) { KeyboardState.Left = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.Down) { KeyboardState.Down = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.Right) { KeyboardState.Right = KeyState.Up; }
            if (e.Key == Windows.System.VirtualKey.Space) { KeyboardState.Space = KeyState.Up; }
        }

    }
}