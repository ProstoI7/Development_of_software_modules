using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Character
    {
        private char _symbol;
        private string _font;
        private int _fontSize;

        public Character(char symbol, string font, int fontSize)
        {
            _symbol = symbol;
            _font = font;
            _fontSize = fontSize;
        }

        public void Display(int x, int y)
        {
            Console.WriteLine($"Символ '{_symbol}' [Шрифт: {_font}, Размер: {_fontSize}] отрисован в координатах ({x}, {y})");
        }
    }
}
