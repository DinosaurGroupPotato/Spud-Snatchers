using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpudSnatch.Model;

namespace SpudSnatch
{

    public sealed partial class GamePage : Page
    {

        // Instance variables for the GameController
        private GameController ctrl;

        // Timer to keep track of updates
        DispatcherTimer timer;

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

        private void Timer_Tick(object sender, object e)
        {
            throw new NotImplementedException();
        }


    }
}
