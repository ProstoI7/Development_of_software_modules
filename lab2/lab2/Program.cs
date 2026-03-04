using System;
using System.Collections.Generic;

namespace SOLID_Fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            // Общий объект заказа для тестов
            var sampleOrder = new Order
            {
                Id = 1,
                TotalAmount = 250.75m,
                PaymentMethod = "Credit Card",
                Items = new List<string> { "Laptop", "Mouse" },
                CustomerEmail = "test@example.com",
                CustomerPhone = "+1234567890"
            };

            // ==========================================
            // 1. Проверка обработки заказов (SRP и DIP)
            // ==========================================
            Console.WriteLine("=== 1. ORDER PROCESSING TEST ===");

            // Ручное внедрение зависимостей (Dependency Injection)
            IOrderRepository repository = new InMemoryOrderRepository();
            IPaymentProcessor paymentProcessor = new DefaultPaymentProcessor();
            IInventoryService inventoryService = new InventoryService();
            // Обратите внимание: тут мы используем EmailNotificationService, реализующий INotificationService
            INotificationService orderNotification = new EmailNotificationService();
            ILogger logger = new ConsoleLogger();
            IReceiptGenerator receiptGenerator = new ReceiptGenerator();

            OrderProcessor processor = new OrderProcessor(
                repository, paymentProcessor, inventoryService, orderNotification, logger, receiptGenerator);

            repository.Add(sampleOrder);
            processor.ProcessOrder(1);

            OrderReportService reportService = new OrderReportService(repository);
            reportService.GenerateMonthlyReport();

            Console.WriteLine();

            // ==========================================
            // 2. Проверка банковских счетов (LSP)
            // ==========================================
            Console.WriteLine("=== 2. BANK ACCOUNTS TEST ===");

            Bank bank = new Bank();
            SavingsAccount savings = new SavingsAccount();
            savings.Deposit(500m);

            Console.WriteLine($"Savings balance: {savings.Balance}");
            bank.ProcessWithdrawal(savings, 200m); // Успешно
            bank.ProcessWithdrawal(savings, 350m); // Ошибка: нарушение минимального баланса (100)

            CheckingAccount checking = new CheckingAccount();
            checking.Deposit(100m);
            bank.ProcessWithdrawal(checking, 500m); // Успешно, так как есть овердрафт 500

            Console.WriteLine();

            // ==========================================
            // 3. Проверка управления заказами (ISP)
            // ==========================================
            Console.WriteLine("=== 3. ORDER MANAGEMENT TEST ===");

            // Клиентский портал имеет доступ только к базовым действиям
            IOrderBaseActions customerPortal = new CustomerPortal();
            customerPortal.CreateOrder(sampleOrder);

            // Менеджер имеет доступ ко всему
            OrderManager manager = new OrderManager();
            manager.ProcessPayment(sampleOrder);
            manager.BackupDatabase();

            Console.WriteLine();

            // ==========================================
            // 4. Проверка сервиса уведомлений (DIP)
            // ==========================================
            Console.WriteLine("=== 4. NOTIFICATION SERVICE TEST ===");

            IEmailService emailService = new EmailService();
            ISmsService smsService = new SmsService();

            OrderService orderService = new OrderService(emailService, smsService);
            orderService.PlaceOrder(sampleOrder);

            Console.WriteLine();

            // ==========================================
            // 5. Проверка калькулятора (Паттерн Стратегия / OCP)
            // ==========================================
            Console.WriteLine("=== 5. DISCOUNT & SHIPPING STRATEGIES TEST ===");

            DiscountCalculator calculator = new DiscountCalculator();
            decimal amount = 1000m;

            // Тестируем разные скидки
            IDiscountStrategy vipDiscount = new VipDiscount();
            IDiscountStrategy studentDiscount = new StudentDiscount();

            Console.WriteLine($"Original Amount: {amount:C}");
            Console.WriteLine($"VIP Discount (15%): {calculator.CalculateDiscount(vipDiscount, amount):C}");
            Console.WriteLine($"Student Discount (8%): {calculator.CalculateDiscount(studentDiscount, amount):C}");

            // Тестируем разную доставку
            IShippingStrategy expressShipping = new ExpressShipping();
            IShippingStrategy intlShipping = new InternationalShipping();

            Console.WriteLine($"Express Shipping (10kg): {calculator.CalculateShipping(expressShipping, 10m, "Local"):C}");
            Console.WriteLine($"International Shipping to Europe: {calculator.CalculateShipping(intlShipping, 10m, "Europe"):C}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}