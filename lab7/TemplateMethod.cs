using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab7
{
    public abstract class EventHandlerBase
    {
        protected IFormatStrategy _formatStrategy;

        protected EventHandlerBase(IFormatStrategy strategy)
        {
            _formatStrategy = strategy;
        }

        public void SetStrategy(IFormatStrategy strategy)
        {
            _formatStrategy = strategy;
        }

        public void ProcessEvent(MetricEventArgs e)
        {
            var message = FormatMessage(e.EventType, e.Data);

            SendMessage(message);

            LogResult();
        }

        protected virtual string FormatMessage(string type, object data)
        {
            return _formatStrategy.Format($"{type}: {data}", DateTime.Now);
        }

        protected abstract void SendMessage(string message);

        protected virtual void LogResult()
        {
            Console.WriteLine("[Base]: Обработка события завершена.");
        }

        public class ConsoleHandler : EventHandlerBase
        {
            public ConsoleHandler(IFormatStrategy strategy) : base(strategy) 
            {
            }

            protected override void SendMessage(string message)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ConsoleHandler] ОТПРАВКА: {message}");
                Console.ResetColor();
            }

            protected override void LogResult()
            {
                Console.WriteLine("[ConsoleHandler]: Данные успешно выведены на экран.");
            }
        }

        public class FileHandler : EventHandlerBase
        {
            private readonly string _filePath = "log.txt";

            public FileHandler(IFormatStrategy strategy) : base(strategy) { }

            protected override void SendMessage(string message)
            {
                File.AppendAllText(_filePath, message + Environment.NewLine);
                Console.WriteLine($"[FileHandler] ЗАПИСЬ В ФАЙЛ '{_filePath}': {message}");
            }

            protected override void LogResult()
            {
                Console.WriteLine("[FileHandler]: Запись в лог-файл подтверждена.");
            }
        }
    }
}
