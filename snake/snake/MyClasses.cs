using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

// start project

namespace snake
{
  public enum Direction { Up, Down, Right, Left };

  public class Snake
  {
    double thickness = 1.0;
    MainWindow window;
    int eating = 0;
     int score = 0;

    public int Score { get { return score; } set { } }

    const double speed = 1.0;
    readonly Vector dRight = new Vector(speed, 0);
    readonly Vector dLeft = new Vector(-speed, 0);
    readonly Vector dUp = new Vector(0, -speed);
    readonly Vector dDown = new Vector(0, speed);

    Vector dd;

    public Snake(double thickness, MainWindow window)
    {
      this.thickness = thickness;
      this.window = window;
      dd = dRight;

      snakeLine = new Polyline();
      snakeLine.Stroke = Brushes.Black;
      snakeLine.StrokeThickness = thickness;

      heading = Direction.Right;
      snakeLine.Points.Add(new Point(100.0, 200.0));
      snakeLine.Points.Add(new Point(250.0, 200.0));
    }

    Polyline snakeLine = null;
    public Polyline SnakeLine
    {
      get { return snakeLine; }
    }

    Direction heading;
    public Direction Heading
    {
      get { return heading; }
    }

    public Point Head
    {
      get
      {
        int n = snakeLine.Points.Count - 1;
        return snakeLine.Points[n];
      }
    }

    public void NewHeading(Direction dir)
    {
      heading = dir;

      int n = snakeLine.Points.Count - 1;
      Point h = snakeLine.Points[n];
      snakeLine.Points.Add(h);

      if (dir == Direction.Up) { dd = dUp; }
      if (dir == Direction.Down) { dd = dDown; }
      if (dir == Direction.Left) { dd = dLeft; }
      if (dir == Direction.Right) { dd = dRight; }
    }

    void MoveHead()
    {
      int n = snakeLine.Points.Count - 1;
      Point h = snakeLine.Points[n];
      snakeLine.Points[n] += dd;
    }

    void MoveTail()
    {
      if (snakeLine.Points[0] == snakeLine.Points[1])
        snakeLine.Points.RemoveAt(0);

      double dxTail = Math.Sign(snakeLine.Points[1].X - snakeLine.Points[0].X) * speed;
      double dyTail = Math.Sign(snakeLine.Points[1].Y - snakeLine.Points[0].Y) * speed;
      snakeLine.Points[0] += new Vector(dxTail, dyTail);
    }

    public void Move()
    {
      MoveHead();

      if (eating <= 0)
          MoveTail();
      else
          eating--;

     
      CheckCollision();
    }

    private void CheckCollision()
    {
        if (hasCollidedWithBorder())
            window.EpicFailure();

        if(hasCollidedWithItself())
            window.EpicFailure();

        foreach (Line l in window.Walls)
        {
            if (hasCollidedWithLine(l))
            {
                window.EpicFailure();
                return;
            }
        }

        foreach (Rectangle f in window.Food)
        {
            if (hasCollidedWithFood(f))
           {
               Console.WriteLine("Om nom");
               eat(f);
               break;
            }
        }
    }

    private void eat(Rectangle f)
    {
        window.canvas.Children.Remove(f);
        window.Food.Remove(f);
        window.AddFood();

        eating += (int) (f.Width / speed);
        score++;

    }

    void order(ref double a, ref double b)
    {
      if (a > b)
      {
        double h = a; a = b; b = h;
      }
    }

    private bool hasCollidedWithFood(Rectangle f)
    {
        double x0 = Canvas.GetLeft(f);
        double x1 = x0 + f.Width;
        double y0 = Canvas.GetTop(f);
        double y1 = y0 + f.Height;

        if (Head.X >= x0 && Head.X <= x1 && Head.Y >= y0 && Head.Y <= y1)
            return true;

        return false;
    }

    private bool hasCollidedWithBorder()
    {
        return Head.X >= window.Width || Head.X <= 0 || Head.Y >= window.Height || Head.Y <= 0;
        
    }

    private bool hasCollidedWithHorizontalLine(double x1, double x2, double y, double thickness)
    {
      order(ref x1, ref x2);
      if (x1 > x2) MessageBox.Show(x1 + " " + x2);
      return (x1 <= Head.X && Head.X <= x2 && Math.Abs(Head.Y - y) * 2 < this.thickness + thickness);
    }

    private bool hasCollidedWithVerticalLine(double x, double y1, double y2, double thickness)
    {
      order(ref y1, ref y2);
      if (y1 > y2) MessageBox.Show(y1 + " " + y2);
      return (y1 <= Head.Y && Head.Y <= y2 && Math.Abs(Head.X - x) * 2 < this.thickness + thickness);
    }

    public bool hasCollidedWithLine(Line line)
    {
      if (line.X1 == line.X2)
        return hasCollidedWithVerticalLine(line.X1, line.Y1, line.Y2, line.StrokeThickness);
      else
        if (line.Y1 == line.Y2)
          return hasCollidedWithHorizontalLine(line.X1, line.X2, line.Y1, line.StrokeThickness);
        else
          throw new Exception("Sloping line not permitted here");
    }

    bool hasCollidedBetween(Point p1, Point p2)
    {
      if (p1.X == p2.X)
        return hasCollidedWithVerticalLine(p1.X, p1.Y, p2.Y, thickness);
      else
        if (p1.Y == p2.Y)
          return hasCollidedWithHorizontalLine(p1.X, p2.X, p1.Y, thickness);
        else
          throw new Exception("Sloping line not permitted here");
    }

    public bool hasCollidedWithItself()
    {
      for (int n = 0; n < snakeLine.Points.Count - 4; n++)
      {
        if (hasCollidedBetween(snakeLine.Points[n], snakeLine.Points[n + 1]))
        {
          return true;
        }
      }
      return false;
    }
  }
}
