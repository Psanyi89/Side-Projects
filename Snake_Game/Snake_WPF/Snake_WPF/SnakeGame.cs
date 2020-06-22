using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        public event EventHandler<int> OnFoodEatenEvent;
        public event EventHandler<int> OnGameStatusUpdatedEvent;
        const int SnakeStartintLenght = 3;
        int snakeLenght;
        int currentPoint;
        SolidColorBrush snakeHeadColorBrush = Brushes.YellowGreen;
        SolidColorBrush snakeBodyColorBrush = Brushes.Green;
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
                        originalDirection = SnakeDirections.Up;
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
            throw new NotImplementedException();
        }

        private void StartNewGame()
        {
            throw new NotImplementedException();
        }
    }
}
