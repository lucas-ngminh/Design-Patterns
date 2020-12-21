using System;

namespace _6.BridgePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle(new Red());
            Console.WriteLine(circle.ToString());

            var square = new Square(new Blue());
            Console.WriteLine(square.ToString());
        }
    }

    public interface Color
    {
        string Log();
    }

    public abstract class Shape
    {
        public string Type { get; set; }

        public Color Color { get; set; }

        public Shape(Color color, string type)
        {
            Color = color;
            Type = type;
        }

        public override string ToString()
        {
            return $"I am a {Color.Log()} {Type}";
        }
    }

    public class Red : Color
    {
        public string Log()
        {
            return "Red";
        }
    }

    public class Blue : Color
    {
        public string Log()
        {
            return "Blue";
        }
    }

    public class Circle : Shape
    {
        public Circle(Color color)
            : base (color, "Circle")
        {

        }
    }

    public class Square : Shape
    {
        public Square(Color color)
            : base(color, "Square")
        {

        }
    }
}
