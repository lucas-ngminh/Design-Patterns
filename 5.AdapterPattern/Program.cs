using System;

namespace _5.AdapterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Line line = new Line(5, 2);
            LineToPointAdapter adapter = new LineToPointAdapter(line);

            Console.WriteLine($"---Steps to draw Line ({line.StartX}, {line.StartY})-({line.EndX}, {line.EndY})---");
            adapter.DrawLine();
        }
    }

    public class Line
    {
        public int StartX { get; set; }
        public int StartY = 0;
        public int EndX { get; set; }
        public int EndY = 0;

        public Line (int startX, int endX)
        {
            StartX = startX;
            EndX = endX;
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void DrawPoint()
        {
            Console.WriteLine($"Draw a point at ({X},{Y})");
        }
    }

    public class LineToPointAdapter
    {
        private readonly Line _adaptee;

        public LineToPointAdapter(Line line)
        {
            _adaptee = line;
        }

        public void DrawLine()
        {
            int min = Math.Min(_adaptee.StartX, _adaptee.EndX);
            int max = Math.Max(_adaptee.StartX, _adaptee.EndX);

            for (int i = min; i <= max; i++)
            {
                Point p = new Point(i, _adaptee.StartY);
                p.DrawPoint();
            }
        }
    }
}
