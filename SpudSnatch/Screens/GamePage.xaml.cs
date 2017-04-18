using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpudSnatch.Model;
using SpudSnatch.Model.Objects;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;
using SpudSnatch.State;
using SpudSnatch.Screens;
using Windows.UI.Core;

namespace SpudSnatch
{

    public sealed partial class GamePage : Page
    {
        
        // Timer to keep track of updates
        DispatcherTimer timer;
        private int gameTime = 0;
        // Labels for score and time
        TextBlock ScoreLabel;
        TextBlock TimeLabel;

        // Lists of image objects
        List<Image> Potatoes, Obstacles, Enemies, Platforms;
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

            Window.Current.CoreWindow.KeyDown += CheckKeyDown;
            Window.Current.CoreWindow.KeyUp += CheckKeyUp;

            GameController.Instance.level.Player.HomerUpdated += UpdateHomer;
            
            SetUpImages();
            KeyboardState.InitializeKeys();
        }

        private void SetUpImages()
        {
            // Add potatoes
            Potatoes = new List<Image>();
            foreach (Potato potato in GameController.Instance.level.GetPotatoes())
            {
                Image PotatoImage = new Image();
                PotatoImage.Margin = new Windows.UI.Xaml.Thickness(potato.PositionX, potato.PositionY, 0, 0);
                PotatoImage.Tag = potato.ID;
                PotatoImage.Width = 20;
                PotatoImage.Height = 20;
                PotatoImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Potato/Potato.png"));
                Potatoes.Add(PotatoImage);
                GameGrid.Children.Add(PotatoImage);
            }
            // Add enemies
            Enemies = new List<Image>();
            foreach (Enemy enemy in GameController.Instance.level.GetEnemies())
            {
                Image EnemyImage = new Image();
                EnemyImage.Margin = new Windows.UI.Xaml.Thickness(enemy.PositionX, enemy.PositionY, 0, 0);
                EnemyImage.Tag = enemy.ID;
                EnemyImage.Width = 50;
                EnemyImage.Height = 50;
                EnemyImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Enemy/StaticImages/Enemy_standing.png"));
                Enemies.Add(EnemyImage);
                GameGrid.Children.Add(EnemyImage);
            }
            // Add Obstacles
            Obstacles = new List<Image>();
            foreach (Obstacle obstacle in GameController.Instance.level.GetObstacles())
            {
                Image obstacleImage = new Image();
                obstacleImage.Margin = new Windows.UI.Xaml.Thickness(obstacle.positionX, obstacle.positionY, 0, 0);
                obstacleImage.Tag = obstacle.ID;
                if (obstacle is PlatformObstacle)
                {
                    obstacleImage.Width = 150;
                    obstacleImage.Height = 100;
                    obstacleImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Platform/Platform.png"));
                }
                else if (obstacle is Wall)
                {
                    obstacleImage.Width = 150;
                    obstacleImage.Height = 150;
                    obstacleImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Wall/wall.png"));
                }
                else
                {
                    obstacleImage.Width = 150;
                    obstacleImage.Height = 100;
                    obstacleImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Window/window.png"));
                }
                Obstacles.Add(obstacleImage);
                GameGrid.Children.Add(obstacleImage);
            }
            // Add Homer
            Homer = new Image();
            Homer.Margin = new Windows.UI.Xaml.Thickness(GameController.Instance.level.Player.PositionX, GameController.Instance.level.Player.PositionY, 0, 0);
            Homer.Width = 50;
            Homer.Height = 50;
            Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/stand.jpg"));
            GameGrid.Children.Add(Homer);

            // Add Score and Time labels
            ScoreLabel = new TextBlock();
            ScoreLabel.HorizontalAlignment = HorizontalAlignment.Left;
            ScoreLabel.VerticalAlignment = VerticalAlignment.Top;
            GameGrid.Children.Add(ScoreLabel);
            TimeLabel = new TextBlock();
            TimeLabel.HorizontalAlignment = HorizontalAlignment.Right;
            TimeLabel.VerticalAlignment = VerticalAlignment.Top;
            GameGrid.Children.Add(TimeLabel);
        }

        private void Timer_Tick(object sender, object e)
        {
            // Re-focus the grid for the keyboard update methods

            // Update objects (using KeyBoardState)

            GameController.Instance.level.Player.Update();
            GameController.Instance.UpdateGameController();
            UpdateScore();
            UpdateTime();
            HomerAnimations();
        }

        private void HomerAnimations()
        {
            switch (GameController.Instance.level.Player.State)
            {
                case HomerState.Jumping:
                    Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/jump_left.png"));
                    //Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/jump.gif"));
                    break;
                case HomerState.Ducking:
                    Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/duck.jpg"));
                    break;
                default:
                    Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/stand.jpg"));
                    break;
            }
        }

        private void UpdateTime()
        {
            gameTime++;
            TimeLabel.Text = "Time: " + Convert.ToString(gameTime);
        }

        private void UpdateScore()
        {
            ScoreLabel.Text = "Score: " + Convert.ToString(GameController.Instance.Score);
            if (GameController.Instance.Score > 1000)
            {
                Frame.Navigate(typeof(EndScreen));
            }
        }

        private void UpdatePotatoes(int id)
        {
            Potato updatedPotato;
            for (int i = 0; i < GameController.Instance.level.potatoes.Count; i++)
            {
                if (GameController.Instance.level.potatoes[i].ID == id)
                {
                    updatedPotato = GameController.Instance.level.potatoes[i];
                    break;
                }
            }
        }

        private void UpdateHomer(object sender, EventArgs e)
        {
            Homer.Margin = new Windows.UI.Xaml.Thickness(GameController.Instance.level.Player.PositionX, GameController.Instance.level.Player.PositionY, 0, 0);
        }

        private void UpdateObjects(int id)
        {

        }

        // Handles the key-up event and sets the values in the keyboard state accordingly
        private void CheckKeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.W) { KeyboardState.W = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.A) { KeyboardState.A = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.S) { KeyboardState.S = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.D) { KeyboardState.D = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.Up) { KeyboardState.Up = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.Left) { KeyboardState.Left = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.Down) { KeyboardState.Down = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.Right) { KeyboardState.Right = KeyState.Down; }
            if (e.VirtualKey == Windows.System.VirtualKey.Space) { KeyboardState.Space = KeyState.Down; }
        }

        // Handles the key-down event and sets the values in the keyboard state accordingly
        private void CheckKeyUp(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.W) { KeyboardState.W = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.A) { KeyboardState.A = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.S) { KeyboardState.S = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.D) { KeyboardState.D = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.Up) { KeyboardState.Up = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.Left) { KeyboardState.Left = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.Down) { KeyboardState.Down = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.Right) { KeyboardState.Right = KeyState.Up; }
            if (e.VirtualKey == Windows.System.VirtualKey.Space) { KeyboardState.Space = KeyState.Up; }
        }

    }
}