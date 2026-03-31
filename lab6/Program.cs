namespace lab6
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Проверка Flyweight (Символы) ===");
            CharacterFactory charFactory = new CharacterFactory();

            var char1 = charFactory.GetCharacter('A', "Arial", 12);
            var char2 = charFactory.GetCharacter('B', "Arial", 12);
            var char3 = charFactory.GetCharacter('A', "Arial", 12);

            char1.Display(10, 10);
            char2.Display(10, 10);
            char3.Display(20, 10);

            Console.WriteLine($"Всего создано объектов в памяти: {charFactory.GetCount()}");
            Console.WriteLine();


            Console.WriteLine("=== Работа с документом ===");

            IRenderingEngine screenEngine = new ScreenRenderer();
            Document myDoc = new Document(screenEngine);

            Page page1 = myDoc.CreatePage();

            page1.Add(new Line(0, 0, 100, 100, screenEngine));

            IDrawable imageProxy = new ImageProxy("vacation.jpg");

            IDrawable decoratedImage = new BorderDecorator(
                new ShadowDecorator(imageProxy, 5),
                2
            );

            page1.Add(decoratedImage);

            IDrawable circle = new Ellipse(50, 50, 30, 30, screenEngine);
            page1.Add(new TransparencyDecorator(circle, 0.5f));

            myDoc.RenderAll();
        }
    }
}
