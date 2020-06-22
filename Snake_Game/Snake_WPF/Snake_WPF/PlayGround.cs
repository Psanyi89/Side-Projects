using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake_WPF
{
    public class PlayGround
    {
        private readonly Canvas _canvas;
        private readonly int _squareSize;
        private readonly bool _isChessboard;

        public PlayGround(Canvas canvas, int squareSize, bool isChessboard = false)
        {
            _canvas = canvas;
            _squareSize = squareSize;
            _isChessboard = isChessboard;
        }

        public void Draw()
        {
            bool isStillDrawing = true;
            int nextX = 0;
            int nextY = 0;
            bool isOddNumber = true;
            int rowCounter = 1;
            while (isStillDrawing)
            {
                Rectangle rectangle = new Rectangle
                {
                    Height = _squareSize,
                    Width = _squareSize,
                    Stroke = Brushes.Black,
                    StrokeThickness = 0.4
                };
                if (_isChessboard)
                {
                    rectangle.Fill = isOddNumber ? Brushes.Black : Brushes.White;
                    isOddNumber = !isOddNumber;
                }
                _canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, nextX);
                Canvas.SetTop(rectangle, nextY);
                nextX += _squareSize;
                

                if (nextX>=_canvas.ActualWidth)
                {
                    nextX = 0;
                    nextY += _squareSize;
                    rowCounter++;
                    isOddNumber = rowCounter % 2 > 0;

                }
                if (nextY>=_canvas.ActualHeight)
                {
                    isStillDrawing = false;
                }
            }
        }
    }
}