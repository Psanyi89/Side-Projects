using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
namespace Snake_WPF
{
   public class SnakeGame
    {
        private readonly int _squareSize;
        private readonly Canvas _canvas;
        SnakeDirections SnakeDirection=SnakeDirections.Right;
        List<SnakePart> snakeParts = new List<SnakePart>();
        public event EventHandler OnStartNewGameEvent;
        public event EventHandler OnGameOverEvent;
        public event EventHandler<GameStatusEventArgs> OnFoodEatenEvent;
        public event EventHandler<GameStatusEventArgs> OnGameStatusUpdatedEvent;
        const int SnakeStartintLenght = 3;
        int snakeLenght;
        int currentPoint;
        Random Random = new Random();
        SnakePart snakeFood;
        SolidColorBrush snakeHeadColorBrush = Brushes.YellowGreen;
        SolidColorBrush snakeBodyColorBrush = Brushes.Green;
        SolidColorBrush snakeFoodColorBrush = Brushes.Red;
        public SnakeGame(Canvas canvas,int squareSize)
        {
            _canvas = canvas;
            _squareSize = squareSize;
            
        }

        public void OnKeyDown(Key e)
        {
            SnakeDirections originalDirection = SnakeDirection;

            switch (e)
            {
                case Key.Right:
                    if (originalDirection!=SnakeDirections.Left)
                    {
                        SnakeDirection = SnakeDirections.Right;
                    }
                    break;
                case Key.Left:
                    if (originalDirection!=SnakeDirections.Right)
                    {
                       SnakeDirection= SnakeDirections.Left;
                    }
                    break;
                case Key.Down:
                    if (originalDirection!=SnakeDirections.Up)
                    {
                        SnakeDirection = SnakeDirections.Down;
                    }
                    break;
                case Key.Up:
                    if (originalDirection!=SnakeDirections.Down)
                    {
                        SnakeDirection = SnakeDirections.Up;
                    }
                    break;
                case Key.Space:
                   StartNewGame();
                    break;
            }

            if (SnakeDirection!=originalDirection)
            {
                MoveSnake();
            }
        }

        public void MoveSnake()
        {

            try
            {
                while (snakeParts.Count >= snakeLenght)
                {
                    _canvas.Children.Remove(snakeParts[0].UiElement);
                    snakeParts.RemoveAt(0);
                }

                foreach (SnakePart snakePart in snakeParts)
                {
                    snakePart.IsHead = false;
                    (snakePart.UiElement as Rectangle).Fill = snakeBodyColorBrush;
                }

                SnakePart snakeHead = snakeParts.Last();
                int nextX = (int)snakeHead.Position.X;
                int nextY = (int)snakeHead.Position.Y;
                switch (SnakeDirection)
                {
                    case SnakeDirections.Left:
                        nextX -= _squareSize;
                        break;
                    case SnakeDirections.Right:
                        nextX += _squareSize;
                        break;
                    case SnakeDirections.Up:
                        nextY -=_squareSize;
                        break;
                    case SnakeDirections.Down:
                        nextY += _squareSize;
                        break;
                }
                SnakePart newSnakeHead = new SnakePart
                {
                    IsHead = true,
                    Position = new Point(nextX, nextY),

                };
                snakeParts.Add( newSnakeHead);
                DrawSnake();
                DoCheckCollision();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void DoCheckCollision()
        {
            try
            {
                SnakePart snakeHead = snakeParts.First(x => x.IsHead);

                if (snakeHead.Position.X == snakeFood?.Position.X && snakeHead.Position.Y == snakeFood.Position.Y)
                {
                    OnFoodEaten();
                }

                if (snakeHead.Position.X >= _canvas.ActualWidth || snakeHead.Position.X < 0
                    || snakeHead.Position.Y >= _canvas.ActualHeight || snakeHead.Position.Y < 0
                    || snakeParts.Where(x => !x.IsHead && x.Position.X == snakeHead.Position.X && x.Position.Y == snakeHead.Position.Y).Count() > 0)
                {
                    OnGameOver();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void OnFoodEaten()
        {
            snakeLenght++;
            currentPoint++;
            OnFoodEatenEvent?.Invoke(this, new GameStatusEventArgs(currentPoint));
            _canvas.Children.Remove(snakeFood.UiElement);
            DrawSnakeFood();
            UpdateGameStatus();
        }
        protected virtual void OnNewGameStarted()
        {
            OnStartNewGameEvent?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void UpdateGameStatus()
        {
            OnGameStatusUpdatedEvent?.Invoke(this, new GameStatusEventArgs(currentPoint));
        }

        private void OnGameOver()
        {
            try
            {
                OnGameOverEvent?.Invoke(this, EventArgs.Empty);
                MessageBox.Show($"Game Over");
                StartNewGame();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void DrawSnake()
        {
            foreach (SnakePart snakePart in snakeParts)
            {
                if (snakePart.UiElement==null)
                {
                    snakePart.UiElement = new Rectangle
                    {
                        Width = _squareSize,
                        Height = _squareSize,
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.4,
                        Fill = snakePart.IsHead ? snakeHeadColorBrush : snakeBodyColorBrush
                    };
                _canvas.Children.Add(snakePart.UiElement);
                Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
                Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X);
                }

            }
        }

        public void StartNewGame()
        {
            try
            {
                currentPoint = 0;
                snakeLenght = SnakeStartintLenght;
                SnakeDirection = SnakeDirections.Right;
                if (snakeParts != null)
                {
                    foreach (SnakePart snakePart in snakeParts)
                    {
                        if (_canvas.Children.Contains(snakePart.UiElement))
                        {
                            _canvas.Children.Remove(snakePart.UiElement);
                        }
                    }
                    snakeParts.Clear();
                }
                snakeParts.Add(new SnakePart { Position = new Point(_squareSize * 5, _squareSize * 5) });
                DrawSnake();
                if (snakeFood!=null && _canvas.Children.Contains(snakeFood.UiElement))
                {
                    _canvas.Children.Remove(snakeFood.UiElement);
                    snakeFood = null;    
                }
                DrawSnakeFood();
                OnNewGameStarted();
                UpdateGameStatus();
            }
            catch ( Exception ex)
            {

                throw;
            }
        }

        private void DrawSnakeFood()
        {
            Point foodPosition = GetFoodPosition();

            snakeFood = new SnakePart
            {
                Position = foodPosition,
                UiElement=new Ellipse
                {
                    Width=_squareSize,
                    Height=_squareSize,
                    Fill=snakeFoodColorBrush                     
                }
            };
            _canvas.Children.Add(snakeFood.UiElement);
            Canvas.SetTop(snakeFood.UiElement, snakeFood.Position.Y);
            Canvas.SetLeft(snakeFood.UiElement, snakeFood.Position.X);
        }

        private Point GetFoodPosition()
        {
            int maxX = (int)(_canvas.ActualWidth / _squareSize);
            int maxY = (int)(_canvas.ActualHeight / _squareSize);
            int foodX = Random.Next(0, maxX) * _squareSize;
            int foodY = Random.Next(0, maxY) * _squareSize;
            foreach (SnakePart snakePart in snakeParts)
            {
                if (snakePart.Position.X == foodX && snakePart.Position.Y == foodY)
                {
                    return GetFoodPosition();
                }
            }
            return new Point(foodX, foodY);
        }
    }
}
