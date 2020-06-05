using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Canvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle mainRectangle;
        Random randomGenerator = new Random();

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

        private Ellipse AddCircle(int x, int y)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 15;
            ellipse.Height = 15;
            ellipse.Fill = new SolidColorBrush(Colors.LightBlue);
            ellipse.StrokeThickness = 1;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);

            System.Windows.Controls.Canvas.SetLeft(ellipse, x);
            System.Windows.Controls.Canvas.SetTop(ellipse, y);
            MainCanvas.Children.Add(ellipse);

            return ellipse;
        }


        private void MoveRectangle(Rectangle rect, Key direction)
        {
            var actualPositionX = System.Windows.Controls.Canvas.GetLeft(rect);
            var actualPositionY = System.Windows.Controls.Canvas.GetTop(rect);


            switch (direction)
            {
                case Key.A:
                case Key.Left:
                    System.Windows.Controls.Canvas.SetLeft(rect, actualPositionX - 10);
                    break;
                case Key.W:
                case Key.Up:
                    System.Windows.Controls.Canvas.SetTop(rect, actualPositionY - 10);
                    break;
                case Key.D:
                case Key.Right:
                    System.Windows.Controls.Canvas.SetLeft(rect, actualPositionX + 10);
                    break;
                case Key.S:
                case Key.Down:
                    System.Windows.Controls.Canvas.SetTop(rect, actualPositionY + 10);
                    break;
                

                default:
                    break;
            }
        }

        private void MainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            MoveRectangle(mainRectangle, e.Key);
        }

        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(MainCanvas);

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(5000000),DispatcherPriority.Normal,Timer_Elapsed,Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            var x = randomGenerator.Next(0, 780);
            var y = randomGenerator.Next(0, 780);

           AddCircle(x, y);
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
