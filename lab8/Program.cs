namespace lab8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var logger = new Logger();
            var queue = new PrintQueue();
            var printer = new Printer();
            var dispatcher = new Dispatcher();

            var mediator = new PrintSystemMediator(printer, queue, logger);
            dispatcher.SetMediator(mediator);

            // Создание документов
            var doc1 = new Document("Важный_документ.docx");
            doc1.SetMediator(mediator);
            var doc2 = new Document("Чертеж.pdf");
            doc2.SetMediator(mediator);

            Console.WriteLine("--- СЦЕНАРИЙ 1: Успешная печать ---");
            doc1.AddToQueue();
            dispatcher.CommandProcessQueue();

            Console.WriteLine("\n--- СЦЕНАРИЙ 2: Ошибка принтера и восстановление ---");
            printer.SimulateFailure = true;
            doc2.AddToQueue();
            dispatcher.CommandProcessQueue();

            Console.WriteLine("\nПопытка снова отправить сломанный документ в очередь:");
            doc2.AddToQueue();

            Console.WriteLine("\nСброс документа и повторная печать:");
            doc2.Reset();
            doc2.AddToQueue();
            dispatcher.CommandProcessQueue();

            Console.WriteLine("\n--- СЦЕНАРИЙ 3: Проверка финального состояния ---");
            doc1.AddToQueue();
            doc1.Print();
            Console.WriteLine("(Ничего не произошло, так как документ в состоянии Done)");
        }
    }
}
