using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpudSnatch.Model;
using SpudSnatch.Model.Objects;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

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
        List<Image> Potatoes;
        // List<Image> Enemies; // (No enemies yet)
        List<Image> Obstacles;
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
        }

        private void SetUpImages()
        {
            // Add potatoes
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
            // Add Obstacles
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
            Homer.Margin = new Windows.UI.Xaml.Thickness(GameController.level.ReturnPlayerPosition("x", GameController.level.GetHomer()), GameController.level.ReturnPlayerPosition("y", GameController.level.GetHomer()), 0, 0);
            Homer.Width = 50;
            Homer.Height = 50;
            Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/stand.jpg"));
            GameGrid.Children.Add(Homer);
        }

        private void Timer_Tick(object sender, object e)
        {
            // Update objects
        }

    }
}