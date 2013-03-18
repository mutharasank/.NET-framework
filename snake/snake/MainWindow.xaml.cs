using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace snake
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    DispatcherTimer timer = new DispatcherTimer();
    Snake snake = null;
    double thickness = 3.0;
    bool paused = false;
    List<Line> walls;
    List<Rectangle> food;

    public List<Line> Walls { get{ return walls; } set{ walls = value; } }
    public List<Rectangle> Food { get { return food; } set { food = value; } }

    private void Window_Loaded_1(object sender, RoutedEventArgs e)
    {
        this.ResizeMode = ResizeMode.NoResize;

      snake = new Snake(thickness, this);
      walls = new List<Line>();
      food = new List<Rectangle>();
      
      Line l1 = new Line();
      l1.Stroke = Brushes.HotPink;
      l1.X1 = 50;
      l1.X2 = 100;
      l1.Y1 = 30;
      l1.Y2 = 30;
      l1.StrokeThickness = 3;
      walls.Add(l1);

      AddFood();

      

      canvas.Children.Add(snake.SnakeLine);
        foreach(Line l in walls){
            canvas.Children.Add(l);
        }
        

      timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
      timer.Tick += timer_Tick;
      timer.Start();
    }

    public void AddFood()
    {
        Random r = new Random();
       
        Rectangle r1 = new Rectangle();
        
        r1.Stroke = Brushes.IndianRed;
        r1.Fill = Brushes.IndianRed;
        r1.Width = r1.Height = r.Next(8, 25);
        Canvas.SetLeft(r1, r.Next(50, (int)this.Width - 50));
        Canvas.SetTop(r1, r.Next(50, (int)this.Height - 50));

        food.Add(r1);
        canvas.Children.Add(r1);
       
    }

    void timer_Tick(object sender, EventArgs e)
    {
      snake.Move();
      statusTBlck.Text = "Head: (" + snake.Head.X + "," + snake.Head.Y + "), Score: "+snake.Score;
    }

    public void EpicFailure()
    {
        MessageBox.Show("Be aware of the obstacles, Idiot!", "Fail!!!", MessageBoxButton.OK, MessageBoxImage.Error);
        timer.Stop();
    }

    private void Window_KeyDown_1(object sender, KeyEventArgs e)
    {
        if (!paused)
        {
            if (e.Key == Key.Down && snake.Heading != Direction.Up)
                snake.NewHeading(Direction.Down);

            if (e.Key == Key.Up && snake.Heading != Direction.Down)
                snake.NewHeading(Direction.Up);

            if (e.Key == Key.Left && snake.Heading != Direction.Right)
                snake.NewHeading(Direction.Left);

            if (e.Key == Key.Right && snake.Heading != Direction.Left)
                snake.NewHeading(Direction.Right);
        }

        if (e.Key == Key.P && !paused)
        {
            timer.Stop();
            paused = true;
        }
        else if (e.Key == Key.P && paused)
        {
            timer.Start();
            paused = false;
        }

    }
  }
}
