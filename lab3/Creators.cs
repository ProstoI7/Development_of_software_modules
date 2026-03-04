using System.Windows.Media;

namespace Lab3
{
    public abstract class CircleCreator
    {
        public abstract Circle CreateCircle();
    }
    public class RedCircleCreator : CircleCreator 
    {
        public override Circle CreateCircle() => new Circle { Color = Colors.Red };
    }
    public class BlueCircleCreator : CircleCreator 
    {
        public override Circle CreateCircle() => new Circle { Color = Colors.Blue };
    }
    public class GreenCircleCreator : CircleCreator 
    {
        public override Circle CreateCircle() => new Circle { Color = Colors.Green };
    }

    public abstract class SquareCreator
    {
        public abstract Square CreateSquare();
    }
    public class RedSquareCreator : SquareCreator 
    {
        public override Square CreateSquare() => new Square { Color = Colors.Red };
    }
    public class BlueSquareCreator : SquareCreator
    {
        public override Square CreateSquare() => new Square { Color = Colors.Blue };
    }
    public class GreenSquareCreator : SquareCreator
    {
        public override Square CreateSquare() => new Square { Color = Colors.Green };
    }

    public abstract class TriangleCreator
    {
        public abstract Triangle CreateTriangle();
    }
    public class RedTriangleCreator : TriangleCreator
    {
        public override Triangle CreateTriangle() => new Triangle { Color = Colors.Red };
    }
    public class BlueTriangleCreator : TriangleCreator
    {
        public override Triangle CreateTriangle() => new Triangle { Color = Colors.Blue };
    }
    public class GreenTriangleCreator : TriangleCreator
    {
        public override Triangle CreateTriangle() => new Triangle { Color = Colors.Green };
    }
}