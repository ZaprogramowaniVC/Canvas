using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Canvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle mainRectangle;

        public MainWindow()
        {
            InitializeComponent();
            mainRectangle = AddRectangle(390, 390);
        }

        private Rectangle AddRectangle(int x, int y)
        {
            Rectangle rect = new Rectangle();
            rect.Width = 20;
            rect.Height = 20;
            rect.Fill = new SolidColorBrush(Colors.Red);

            System.Windows.Controls.Canvas.SetLeft(rect, x);
            System.Windows.Controls.Canvas.SetTop(rect, y);
            MainCanvas.Children.Add(rect);

            return rect;
        }


        private void MoveRectangle(Rectangle rect, Direction direction)
        {
            var actualPositionX = System.Windows.Controls.Canvas.GetLeft(rect);
            var actualPositionY = System.Windows.Controls.Canvas.GetTop(rect);


            switch (direction)
            {
                case Direction.Left:
                    System.Windows.Controls.Canvas.SetLeft(rect, actualPositionX - 10);
                    break;
                case Direction.Up:
                    System.Windows.Controls.Canvas.SetTop(rect, actualPositionY + 10);
                    break;
                case Direction.Right:
                    System.Windows.Controls.Canvas.SetLeft(rect, actualPositionX + 10);
                    break;
                case Direction.Down:
                    System.Windows.Controls.Canvas.SetTop(rect, actualPositionY - 10);
                    break;
                default:
                    break;
            }
        }

    }

    enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
}
