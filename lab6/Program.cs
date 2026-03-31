namespace lab6
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ЭТАП 1. Паттерн «Приспособленец» (Flyweight)");

            CharacterFactory charFactory = new CharacterFactory();

            Console.WriteLine("--- Запрашиваем символы из фабрики ---");
            Character char1 = charFactory.GetCharacter('A', "Arial", 12);
            Character char2 = charFactory.GetCharacter('B', "Arial", 12);
            Character char3 = charFactory.GetCharacter('A', "Times New Roman", 14);
            Character char4 = charFactory.GetCharacter('A', "Arial", 12);

            char1.Display(10, 20);
            char2.Display(10, 40);
            char4.Display(70, 20);

            Console.WriteLine($"Всего создано уникальных объектов в памяти: {charFactory.GetCount()}");

            Console.WriteLine("ЭТАП 2. Паттерн «Заместитель» (Proxy)");

            Console.WriteLine("1. Создаём прокси:");
            ImageProxy img1 = new ImageProxy("vacation_photo.jpg");
            ImageProxy img2 = new ImageProxy("work_document.png");
            ImageProxy img3 = new ImageProxy("unused_file.bmp");

            Console.WriteLine("\n2. Запрашиваем данные:");
            Console.WriteLine($"   Ширина img1: {img1.GetWidth()}px");

            Console.WriteLine("\n3. Отрисовываем второе изображение:");
            img2.Draw();

            Console.WriteLine("\n4. Снова обращаемся к img1 (уже загружено, мгновенно):");
            Console.WriteLine($"   Высота img1: {img1.GetHeight()}px");

            Console.WriteLine("\n5. img3 так и не был вызван - память на него не тратилась.");


            Console.WriteLine("ЭТАП 3. Паттерн «Мост» (Bridge)");

            IRenderingEngine screenEngine = new ScreenRenderer();
            IRenderingEngine printEngine = new PrintRenderer();

            Console.WriteLine("1. Создаём фигуры для экрана и для печати:");
            Rectangle screenRect = new Rectangle(10, 20, 100, 50, screenEngine);
            Rectangle printRect = new Rectangle(10, 20, 100, 50, printEngine);

            Console.WriteLine("\n2. Отрисовка одной и той же логической фигуры на разных движках:");
            screenRect.Draw();
            printRect.Draw();

            Console.WriteLine("\n3. Перемещение фигуры:");
            screenRect.Move(5, 5);
            screenRect.Draw();


            Console.WriteLine("ЭТАП 4. Паттерн «Декоратор» (Decorator)");

            Console.WriteLine("1. Базовая фигура:");
            Ellipse simpleEllipse = new Ellipse(50, 50, 30, 20, screenEngine);
            simpleEllipse.Draw();
            Console.WriteLine();

            Console.WriteLine("\n2. Применяем один декоратор (Тень):");
            IDrawable shadowedEllipse = new ShadowDecorator(simpleEllipse, 10);
            shadowedEllipse.Draw();
            Console.WriteLine();

            Console.WriteLine("\n3. Комбинируем декораторы (Рамка + Тень + Прозрачность):");
            IDrawable superEllipse = new TransparencyDecorator(new BorderDecorator(new ShadowDecorator(simpleEllipse, 15), 2), 0.7f);
            superEllipse.Draw();
            Console.WriteLine();


            Console.WriteLine("ИТОГОВАЯ СБОРКА: Документ и страницы");

            Document myDoc = new Document(screenEngine);
            Page page1 = myDoc.CreatePage();

            page1.Add(new Line(0, 0, 50, 50, screenEngine));
            page1.Add(new BorderDecorator(img1, 5));
            page1.Add(superEllipse);

            myDoc.RenderAll();
        }
    }
}
