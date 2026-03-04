using System.Windows.Media;

namespace Lab3
{
    public interface IFigureFactory
    {
        Circle CreateCircle();
        Square CreateSquare();
        Triangle CreateTriangle();
    }

    public class RedFactory : IFigureFactory
    {
        public Circle CreateCircle() => new Circle { Color = Colors.Red };
        public Square CreateSquare() => new Square { Color = Colors.Red };
        public Triangle CreateTriangle() => new Triangle { Color = Colors.Red };
    }

    public class BlueFactory : IFigureFactory
    {
        public Circle CreateCircle() => new Circle { Color = Colors.Blue };
        public Square CreateSquare() => new Square { Color = Colors.Blue };
        public Triangle CreateTriangle() => new Triangle { Color = Colors.Blue };
    }

    public class GreenFactory : IFigureFactory
    {
        public Circle CreateCircle() => new Circle { Color = Colors.Green };
        public Square CreateSquare() => new Square { Color = Colors.Green };
        public Triangle CreateTriangle() => new Triangle { Color = Colors.Green };
    }
}