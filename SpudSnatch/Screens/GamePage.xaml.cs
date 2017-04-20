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
using SpudSnatch.Model.Serialization;


namespace SpudSnatch
{

    public class HighScoreVariables
    {
        public string Score { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
    }

    public sealed partial class GamePage : Page
    {
        
        // Timer to keep track of updates
        DispatcherTimer timer;
        private int gameTime = 0;
        // Labels for score and time
        TextBlock ScoreLabel;
        TextBlock HealthLabel;
        TextBlock CheatLabel;
        TextBlock TimeLabel;
        HighScoreVariables passparams = new HighScoreVariables();

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

            Window.Current.CoreWindow.KeyDown += CheckKeyDown;
            Window.Current.CoreWindow.KeyUp += CheckKeyUp;

            GameController.Instance.level.Player.HomerUpdated += UpdateHomer;
            
            SetUpImages();
            KeyboardState.InitializeKeys();

            switch (GameController.Instance.IsDifficultyLevel())
            {
                case Difficulty.Easy:
                    GameController.Instance.level.Player.Health = 3;
                    break;
                case Difficulty.Medium:
                    GameController.Instance.level.Player.Health = 3;
                    break;
                case Difficulty.Hard:
                    GameController.Instance.level.Player.Health = 2;
                    break;
                case Difficulty.Death:
                    GameController.Instance.level.Player.Health = 1;
                    break;
            }
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
                if (potato.State == PotatoState.Big)
                {
                    PotatoImage.Width = 30;
                    PotatoImage.Height = 30;
                }
                else if (potato.State == PotatoState.SmallPoisoned)
                {
                    PotatoImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Potato/PotatoPoisoned.png"));
                }
                else if (potato.State == PotatoState.BigPoisoned)
                {
                    PotatoImage.Width = 30;
                    PotatoImage.Height = 30;
                    PotatoImage.Source = new BitmapImage(new Uri("ms-appx:///Data/Objects/Potato/PotatoPoisoned.png"));
                }
                
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

            // Add Score, Health, Cheat, and Time labels
            StackPanel labelPanel = new StackPanel();
            labelPanel.Orientation = Orientation.Vertical;
            labelPanel.HorizontalAlignment = HorizontalAlignment.Left;
            labelPanel.VerticalAlignment = VerticalAlignment.Top;
            ScoreLabel = new TextBlock();
            labelPanel.Children.Add(ScoreLabel);
            HealthLabel = new TextBlock();
            labelPanel.Children.Add(HealthLabel);
            CheatLabel = new TextBlock();
            labelPanel.Children.Add(CheatLabel);
            GameGrid.Children.Add(labelPanel);

            TimeLabel = new TextBlock();
            TimeLabel.HorizontalAlignment = HorizontalAlignment.Right;
            TimeLabel.VerticalAlignment = VerticalAlignment.Top;
            GameGrid.Children.Add(TimeLabel);
        }

        private void Timer_Tick(object sender, object e)
        {

            // Update objects (using KeyBoardState)
            
            GameController.Instance.UpdateGameController();

            UpdateScore();
            UpdateTime();
            UpdateHealth();
            UpdateCheat();
            UpdateScene();
            HomerAnimations();

            if (GameController.Instance.GameOver == true)
            {
                GameController.Instance.ResetGame();
                passparams.Score = ScoreLabel.Text;
                passparams.Time = TimeLabel.Text;
                Frame.Navigate(typeof(EndScreen), passparams);
            }
        }

        private void UpdateScene()
        {
            foreach(Potato tater in GameController.Instance.level.potatoes)
            {
                if(tater.Retrieved)
                {
                    foreach(Image chip in Potatoes)
                    {
                        if(Convert.ToInt32(chip.Tag) == tater.ID)
                        {
                          chip.Opacity = 0.0;
                        }
                    }
                    
                }
            }
        }

        private void HomerAnimations()
        {
            switch (GameController.Instance.level.Player.State)
            {
                case HomerState.Jumping:
                    PlayAnimation();
                    //Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/jump_left.png"));
                    //Homer.Source = new BitmapImage(new Uri("pack:///Data/Homer/StaticImages/jump.gif"));
                    break;
                case HomerState.Ducking:
                    Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/duck.jpg"));
                    break;
                default:
                    Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/stand.jpg"));
                    break;
            }
        }

        private void PlayAnimation()
        {
            for (int i = 0; i < 30; i++)
            {
                if (i < 16)
                {
                    Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/jump_left.png"));
                }
                else
                {
                    Homer.Source = new BitmapImage(new Uri("ms-appx:///Data/Homer/StaticImages/jump_right.png"));
                }
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
            if (GameController.Instance.Score > 300)
            {
                GameController.Instance.GameOver = true;
            }
        }

        private void UpdateHealth()
        {
            HealthLabel.Text = "Health: " + GameController.Instance.level.Player.Health.ToString();
        }

        private void UpdateCheat()
        {
            if (GameController.Instance.IsCheatMode)
                CheatLabel.Text = "Cheat Mode Activated";
            else
                CheatLabel.Text = "";
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

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SerializeData.SerializeInfo("SaveData");
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            SerializeData.DeserializeInfo("SaveData");
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
            if (e.VirtualKey == Windows.System.VirtualKey.C) { KeyboardState.C = KeyState.Up; }
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
            if (e.VirtualKey == Windows.System.VirtualKey.C) { KeyboardState.C = KeyState.Up; }
        }

    }
}