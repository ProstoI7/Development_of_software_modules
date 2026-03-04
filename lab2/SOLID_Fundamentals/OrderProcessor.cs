using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLID_Fundamentals
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order GetById(int id);
        IEnumerable<Order> GetAll();
    }

    public interface IPaymentProcessor
    {
        void Process(string paymentMethod, decimal amount);
    }

    public interface IInventoryService
    {
        void Update(List<string> items);
    }

    public interface INotificationService
    {
        void SendMessage(string to, string message);
    }

    public interface ILogger
    {
        void LogInfo(string message);
    }

    public interface IReceiptGenerator
    {
        void Generate(Order order);
    }

    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public void Add(Order order)
        {
            _orders.Add(order);
            Console.WriteLine($"Order {order.Id} added");
        }

        public Order GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

        public IEnumerable<Order> GetAll() => _orders;
    }

    public class DefaultPaymentProcessor : IPaymentProcessor
    {
        public void Process(string paymentMethod, decimal amount)
        {
            Console.WriteLine($"Processing {amount:C} via {paymentMethod}");
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message) => Console.WriteLine($"[LOG]: {message}");
    }

    public class EmailNotificationService : INotificationService
    {
        public void SendMessage(string to, string message) =>
            Console.WriteLine($"Sending Email to {to}: {message}");
    }

    public class InventoryService : IInventoryService
    {
        public void Update(List<string> items) =>
            Console.WriteLine("Inventory updated.");
    }

    public class ReceiptGenerator : IReceiptGenerator
    {
        public void Generate(Order order) =>
            Console.WriteLine($"Receipt generated for order {order.Id}");
    }

    public class OrderProcessor
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IInventoryService _inventoryService;
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger;
        private readonly IReceiptGenerator _receiptGenerator;

        public OrderProcessor(
            IOrderRepository orderRepository,
            IPaymentProcessor paymentProcessor,
            IInventoryService inventoryService,
            INotificationService notificationService,
            ILogger logger,
            IReceiptGenerator receiptGenerator)
        {
            _orderRepository = orderRepository;
            _paymentProcessor = paymentProcessor;
            _inventoryService = inventoryService;
            _notificationService = notificationService;
            _logger = logger;
            _receiptGenerator = receiptGenerator;
        }

        public void ProcessOrder(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                _logger.LogInfo($"Processing order {orderId}");

                if (order.TotalAmount <= 0)
                    throw new ArgumentException("Invalid order amount");

                _paymentProcessor.Process(order.PaymentMethod, order.TotalAmount);
                _inventoryService.Update(order.Items);
                _notificationService.SendMessage(order.CustomerEmail, $"Order {orderId} processed");
                _logger.LogInfo($"Order {orderId} processed at {DateTime.Now}");
                _receiptGenerator.Generate(order);
            }
        }
    }

    public class OrderReportService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderReportService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void GenerateMonthlyReport()
        {
            var orders = _orderRepository.GetAll();
            decimal totalRevenue = orders.Sum(o => o.TotalAmount);
            int totalOrders = orders.Count();
            Console.WriteLine($"Monthly Report: {totalOrders} orders, Revenue: {totalRevenue:C}");
        }
    }

    public class OrderExportService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderExportService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void ExportToExcel(string filePath)
        {
            var orders = _orderRepository.GetAll();
            Console.WriteLine($"Exporting {orders.Count()} orders to {filePath}");
        }
    }
}