using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        DispatcherTimer pointTimer;
        List<Ellipse> pointList;


        public MainWindow()
        {
            InitializeComponent();

            pointList = new List<Ellipse>();

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
                    if(actualPositionX >= 10)
                    System.Windows.Controls.Canvas.SetLeft(rect, actualPositionX - 10);
                    break;
                case Key.W:
                case Key.Up:
                    if (actualPositionY >= 10)
                    System.Windows.Controls.Canvas.SetTop(rect, actualPositionY - 10);
                    break;
                case Key.D:
                case Key.Right:
                    if(actualPositionX <= 760)
                    System.Windows.Controls.Canvas.SetLeft(rect, actualPositionX + 10);
                    break;
                case Key.S:
                case Key.Down:
                    if(actualPositionY <= 730)
                    System.Windows.Controls.Canvas.SetTop(rect, actualPositionY + 10);
                    break;
                

                default:
                    break;
            }
        }

        private void MainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            MoveRectangle(mainRectangle, e.Key);
            var test = pointList.Where(p => Intersect(mainRectangle, p));
            if (test != null)
            {
                foreach (var item in test)
                {
                    MainCanvas.Children.Remove(item);
                }
            }
        }

        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(MainCanvas);

            pointTimer = new DispatcherTimer(new TimeSpan(0, 0, 2), DispatcherPriority.Normal, Timer_Elapsed, Dispatcher.CurrentDispatcher);
            pointTimer.Start();
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            if (pointList.Count >= 3)
            {
                pointTimer.Stop();
                return;
            }

            var x = randomGenerator.Next(0, 760);
            var y = randomGenerator.Next(0, 730);

            pointList.Add(AddCircle(x, y));
        }

        private bool Intersect(Rectangle rect, Ellipse ellipse)
        {
            var ellipseXPosition = System.Windows.Controls.Canvas.GetLeft(ellipse);
            var ellipseYPosition = System.Windows.Controls.Canvas.GetTop(ellipse);
            
            var rectangleXPosition = System.Windows.Controls.Canvas.GetLeft(rect);
            var rectangleYPosition = System.Windows.Controls.Canvas.GetTop(rect);

            if (rectangleXPosition < ellipseXPosition + ellipse.Width && ellipseXPosition < rectangleXPosition + rect.Width)
            return true;

            return false;
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
