using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    public interface IMediator
    {
        void Notify(Colleague sender, string ev, Document document = null);
    }

    public abstract class Colleague
    {
        protected IMediator Mediator;
        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }

    public class Logger : Colleague
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"[Логгер]: {message}");
        }
    }

    public class PrintQueue : Colleague
    {
        private readonly Queue<Document> _queue = new Queue<Document>();
        public bool IsEmpty => _queue.Count == 0;

        public void EnqueueItem(Document document)
        {
            _queue.Enqueue(document);
            Mediator.Notify(this, "Enqueued", document);
        }

        public Document DequeueItem() => _queue.Dequeue();
    }

    public class Printer : Colleague
    {
        public bool SimulateFailure { get; set; } = false;

        public void StartPrint(Document document)
        {
            Console.WriteLine($"[Принтер] Физическая печать '{document.Title}'...");
            if (SimulateFailure)
            {
                SimulateFailure = false;
                Mediator.Notify(this, "PrintFailed", document);
            }
            else
            {
                Mediator.Notify(this, "PrintSuccess", document);
            }
        }
    }

    public class Dispatcher : Colleague
    {
        public void CommandProcessQueue()
        {
            Mediator.Notify(this, "ProcessQueue");
        }
    }
}
