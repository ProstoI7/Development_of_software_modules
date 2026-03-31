using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public abstract class DrawableDecorator : IDrawable
    {
        protected IDrawable _wrappee;
        public DrawableDecorator(IDrawable wrappee)
        {
            _wrappee = wrappee;
        }

        public virtual void Draw()
        {
            _wrappee.Draw();
        }
    }

    public class BorderDecorator : DrawableDecorator
    {
        private int _borderWidth;
        public BorderDecorator(IDrawable wrappee, int borderWidth) : base(wrappee)
        {
            _borderWidth = borderWidth;
        }

        public override void Draw()
        {
            base.Draw();
            Console.Write($" [Рамка толщиной {_borderWidth}]");
        }
    }

    public class ShadowDecorator : DrawableDecorator
    {
        private int _shadowSize;
        public ShadowDecorator(IDrawable wrappee, int shadowSize) : base(wrappee)
        {
            _shadowSize = shadowSize;
        }

        public override void Draw()
        {
            base.Draw();
            Console.Write($" [Тень размером {_shadowSize}]");
        }
    }

    public class TransparencyDecorator : DrawableDecorator
    {
        private float _opacity;
        public TransparencyDecorator(IDrawable wrappee, float opacity) : base(wrappee)
        {
            _opacity = opacity;
        }

        public override void Draw()
        {
            base.Draw();
            Console.Write($" [Прозрачность: {_opacity * 100}%]");
        }
    }


    public class Page
    {
        private List<IDrawable> _drawables = new List<IDrawable>();
        public void Add(IDrawable drawable)
        {
            _drawables.Add(drawable);
        }
        public void Render()
        {
            foreach (var d in _drawables)
            {
                d.Draw();
                Console.WriteLine();
            }
        }
    }

    public class Document
    {
        private List<Page> _pages = new List<Page>();
        private IRenderingEngine _engine;
        public Document(IRenderingEngine engine)
        {
            _engine = engine;
        }
        public Page CreatePage()
        {
            var page = new Page();
            _pages.Add(page);
            return page;
        }
        public void RenderAll()
        {
            _engine.BeginRender();
            for (int i = 0; i < _pages.Count; i++)
            {
                Console.WriteLine($"\n--- Страница {i + 1} ---");
                _pages[i].Render();
            }
            _engine.EndRender();
        }
    }
}
