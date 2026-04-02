using static lab7.EventHandlerBase;

namespace lab7
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Наблюдатель
            EventMonitor monitor = new();

            //Стратегия
            IFormatStrategy textFormat = new TextFormatStrategy();
            IFormatStrategy jsonFormat = new JsonFormatStrategy();
            IFormatStrategy htmlStrategy = new HtmlFormatStrategy();
            //Шаблонный метод
            EventHandlerBase consoleHandler = new ConsoleHandler(textFormat);
            EventHandlerBase fileHandler = new FileHandler(jsonFormat);

            monitor.OnMetricExceeded += consoleHandler.ProcessEvent;
            monitor.OnMetricExceeded += fileHandler.ProcessEvent;

            Console.WriteLine("--- Начинаем мониторинг (Симуляция) ---\n");

            monitor.CheckMetric("CPU_Usage", 45.5, 80.0);

            Console.WriteLine();

            monitor.CheckMetric("RAM_Usage", 92.0, 90.0);

            Console.WriteLine("\n--- Динамическая смена стратегии ---");
            consoleHandler.SetStrategy(jsonFormat);

            monitor.CheckMetric("Disk_Space", 96.0, 95.0);
        }
    }
}
