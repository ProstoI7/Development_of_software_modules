using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace lab6
{
    public interface IDrawable
    {
        void Draw();
    }
    public interface IImage : IDrawable
    {
        int GetWidth();
        int GetHeight();
    }
    public class HighResolutionImage : IImage
    {
        private string _filename;
        private int _width;
        private int _height;
        public HighResolutionImage(string filename)
        {
            _filename = filename;
            Console.Write($"Загрузка {_filename}... ");
            LoadFromDisk();
        }
        private void LoadFromDisk()
        {
            Thread.Sleep(1000);
            _width = 1920;
            _height = 1080;
            Console.WriteLine($"загружено ({_width}x{_height})");
        }
        public void Draw()
        {
            Console.WriteLine($"Отрисовка изображения {_filename}");
        }
        public int GetWidth()
        {
            return _width;
        }
        public int GetHeight()

        {
            return _height;
        }
    }
    public class ImageProxy : IImage
    {
        private string _filename;
        private HighResolutionImage _realImage;

        public ImageProxy(string filename)
        {
            _filename = filename;
            Console.WriteLine($"Создан proxy для {_filename}");
        }

        private void EnsureLoaded()
        {
            if (_realImage == null)
            {
                _realImage = new HighResolutionImage(_filename);
            }
        }

        public void Draw()
        {
            EnsureLoaded();
            _realImage.Draw();
        }

        public int GetWidth()
        {
            EnsureLoaded();
            return _realImage.GetWidth();
        }

        public int GetHeight()
        {
            EnsureLoaded();
            return _realImage.GetHeight();
        }
    }
}
