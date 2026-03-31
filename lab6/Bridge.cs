using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    using System;

    public interface IRenderingEngine
    {
        void BeginRender();
        void EndRender();
        void RenderRectangle(float x, float y, float width, float height);
        void RenderEllipse(float x, float y, float radiusX, float radiusY);
        void RenderLine(float x1, float y1, float x2, float y2);
    }

    public class ScreenRenderer : IRenderingEngine
    {
        public void BeginRender()
        {
            Console.WriteLine("[Screen] Начало рендеринга");
        }

        public void EndRender()
        {
            Console.WriteLine("[Screen] Конец рендеринга");
        }

        public void RenderRectangle(float x, float y, float width, float height)
        {
            Console.WriteLine($"[Screen] Прямоугольник ({x},{y}) размер {width} x{height}");
        }

        public void RenderEllipse(float x, float y, float radiusX, float radiusY)
        {
            Console.WriteLine($"[Screen] Эллипс ({x},{y}) радиусы {radiusX},{radiusY}");
        }

        public void RenderLine(float x1, float y1, float x2, float y2)
        {
            Console.WriteLine($"[Screen] Линия от ({x1},{y1}) до ({x2},{y2})");
        }
    }

    public class PrintRenderer : IRenderingEngine
    {
        public void BeginRender()
        {
            Console.WriteLine("[Print] Начало рендеринга");
        }

        public void EndRender()
        {
            Console.WriteLine("[Print] Конец рендеринга");
        }

        public void RenderRectangle(float x, float y, float width, float height)
        {
            Console.WriteLine($"[Print] Прямоугольник ({x},{y}) размер {width}x{height}");
        }

        public void RenderEllipse(float x, float y, float radiusX, float radiusY)
        {
            Console.WriteLine($"[Print] Эллипс ({x},{y}) радиусы {radiusX},{radiusY}");
        }

        public void RenderLine(float x1, float y1, float x2, float y2)
        {
            Console.WriteLine($"[Print] Линия от ({x1},{y1}) до ({x2},{y2})");
        }
    }

    public abstract class GraphicObject : IDrawable
    {
        protected IRenderingEngine _engine;

        protected GraphicObject(IRenderingEngine engine)
        {
            _engine = engine;
        }

        public abstract void Draw();
        public abstract void Move(float dx, float dy);
    }

    public class Rectangle : GraphicObject
    {
        private float _x, _y, _width, _height;

        public Rectangle(float x, float y, float width, float height, IRenderingEngine engine) : base(engine)
        {
            _x = x; _y = y; _width = width; _height = height;
        }

        public override void Draw()
        {
            _engine.RenderRectangle(_x, _y, _width, _height);
        }

        public override void Move(float dx, float dy)
        {
            _x += dx;
            _y += dy;
        }
    }

    public class Ellipse : GraphicObject
    {
        private float _x, _y, _radiusX, _radiusY;

        public Ellipse(float x, float y, float radiusX, float radiusY, IRenderingEngine engine)
            : base(engine)
        {
            _x = x; _y = y; _radiusX = radiusX; _radiusY = radiusY;
        }

        public override void Draw()
        {
            _engine.RenderEllipse(_x, _y, _radiusX, _radiusY);
        }

        public override void Move(float dx, float dy)
        {
            _x += dx;
            _y += dy;
        }
    }

    public class Line : GraphicObject
    {
        private float _x1, _y1, _x2, _y2;

        public Line(float x1, float y1, float x2, float y2, IRenderingEngine engine)
            : base(engine)
        {
            _x1 = x1; _y1 = y1; _x2 = x2; _y2 = y2;
        }

        public override void Draw()
        {
            _engine.RenderLine(_x1, _y1, _x2, _y2);
        }

        public override void Move(float dx, float dy)
        {
            _x1 += dx; _y1 += dy;
            _x2 += dx; _y2 += dy;
        }
    }
}
