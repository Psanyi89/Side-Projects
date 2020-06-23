using System;
using System.Windows;
using System.Windows.Threading;

namespace Snake_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer DispatcherTimer;
        PlayGround PlayGround;
        SnakeGame SnakeGame;
        const int starterInterval = 400;
        const int SnakeSpeedThreshold = 100;
        const int SquareSize=20;
        public MainWindow()
        {
            InitializeComponent();
            PlayGround = new PlayGround(GameArena, SquareSize);
            SnakeGame = new SnakeGame(GameArena, SquareSize);
            SnakeGame.OnStartNewGameEvent += OnStarNewGame;
            SnakeGame.OnGameOverEvent += OnGameOver;
            SnakeGame.OnFoodEatenEvent += OnFoodEaten;
            SnakeGame.OnGameStatusUpdatedEvent += OnGameStatusUpdate;
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += OnIntervalReached;

        }

        private void OnGameStatusUpdate(object sender, GameStatusEventArgs e)
        {
            Points.Text = e.Score.ToString();
            Speed.Text = DispatcherTimer.Interval.TotalMilliseconds.ToString();
        }

        private void OnFoodEaten(object sender, GameStatusEventArgs e)
        {
           
            int timeInterval = Math.Max(SnakeSpeedThreshold, (int)(DispatcherTimer.Interval.TotalMilliseconds - (e.Score * 2)));
           
            DispatcherTimer.Interval = TimeSpan.FromMilliseconds(timeInterval);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            DispatcherTimer.IsEnabled = false;
        }

        private void OnStarNewGame(object sender, EventArgs e)
        {
            DispatcherTimer.Interval = TimeSpan.FromMilliseconds(starterInterval);
            DispatcherTimer.IsEnabled = true;
        }

        private void OnIntervalReached(object sender, EventArgs e)
        {
            SnakeGame.MoveSnake();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            PlayGround.Draw();
            SnakeGame.StartNewGame();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            SnakeGame.OnKeyDown(e.Key);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
