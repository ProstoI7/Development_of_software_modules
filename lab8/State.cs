using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    public interface IDocumentState
    {
        void Print(Document document);
        void AddToQueue(Document document);
        void CompletePrinting(Document document);
        void FailPrinting(Document document);
        void Reset(Document document);
    }

    public class Document : Colleague
    {
        public string Title { get; }
        private IDocumentState State;

        public Document(string title)
        {
            Title = title;
            SetState(new NewState());
        }

        public void SetState(IDocumentState state) => State = state;

        public void Print() => State.Print(this);
        public void AddToQueue() => State.AddToQueue(this);
        public void CompletePrinting() => State.CompletePrinting(this);
        public void FailPrinting() => State.FailPrinting(this);
        public void Reset() => State.Reset(this);

        public void NotifyMediator(string ev) => Mediator?.Notify(this, ev, this);
    }

    public class NewState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[FSM: New → Printing] Документ '{document.Title}' начинает печать.");
            document.SetState(new PrintingState());
            document.NotifyMediator("RequestPrint");
        }
        public void AddToQueue(Document document)
        {
            Console.WriteLine($"[FSM: New] Документ '{document.Title}' добавлен в очередь.");
            document.NotifyMediator("AddToQueue");
        }
        public void CompletePrinting(Document document)
        {
            Console.WriteLine($"[FSM: New] Игнорирование: Документ '{document.Title}' еще не печатается.");
        }
        public void FailPrinting(Document document)
        {
            Console.WriteLine($"[FSM: New] Игнорирование: Документ '{document.Title}' еще не печатается, ошибка невозможна.");
        }
        public void Reset(Document document)
        {
            Console.WriteLine($"[FSM: New] Игнорирование: Документ '{document.Title}' уже в начальном состоянии.");
        }
    }

    public class PrintingState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[FSM: Printing] Игнорирование: Документ '{document.Title}' уже находится в процессе печати.");
        }
        public void AddToQueue(Document document)
        {
            Console.WriteLine($"[FSM: Printing] Отказ: Документ '{document.Title}' уже печатается, нельзя добавить в очередь.");
        }
        public void CompletePrinting(Document document)
        {
            Console.WriteLine($"[FSM: Printing → Done] Печать документа '{document.Title}' успешно завершена.");
            document.SetState(new DoneState());
        }
        public void FailPrinting(Document document)
        {
            Console.WriteLine($"[FSM: Printing → Error] Произошла ошибка при печати документа '{document.Title}'.");
            document.SetState(new ErrorState());
        }
        public void Reset(Document document)
        {
            Console.WriteLine($"[FSM: Printing] Отказ: Нельзя сбросить документ '{document.Title}' во время печати.");
        }
    }

    public class DoneState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[FSM: Done] Отказ: Документ '{document.Title}' уже напечатан.");
        }
        public void AddToQueue(Document document)
        {
            Console.WriteLine($"[FSM: Done] Отказ: Документ '{document.Title}' уже напечатан, добавление в очередь невозможно.");
        }
        public void CompletePrinting(Document document)
        {
            Console.WriteLine($"[FSM: Done] Игнорирование: Печать документа '{document.Title}' уже была завершена.");
        }
        public void FailPrinting(Document document)
        {
            Console.WriteLine($"[FSM: Done] Игнорирование: Документ '{document.Title}' уже успешно напечатан, ошибка невозможна.");
        }
        public void Reset(Document document)
        {
            Console.WriteLine($"[FSM: Done] Отказ: Документ '{document.Title}' уже напечатан, сброс не требуется.");
        }
    }

    public class ErrorState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[FSM: Error] Печать невозможна из-за ошибки. Сначала сбросьте документ '{document.Title}' (Reset).");
        }
        public void AddToQueue(Document document)
        {
            Console.WriteLine($"[FSM: Error] Нельзя добавить '{document.Title}' в очередь из-за ошибки. Сначала сбросьте документ.");
        }
        public void CompletePrinting(Document document)
        {
            Console.WriteLine($"[FSM: Error] Ошибка '{document.Title}' не устранена. Завершение невозможно.");
        }
        public void FailPrinting(Document document)
        {
            Console.WriteLine($"[FSM: Error] Документ '{document.Title}' уже в состоянии ошибки.");
        }
        public void Reset(Document document)
        {
            Console.WriteLine($"[FSM: Error → New] Документ '{document.Title}' сброшен и готов к повторной работе.");
            document.SetState(new NewState());
        }
    }
}