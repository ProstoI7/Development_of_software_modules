namespace SOLID_Fundamentals
{
    public interface IOrderBaseActions
    {
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }
    public interface IOrderManagement
    {
        void ProcessPayment(Order order);
        void ShipOrder(Order order);
        void GenerateInvoice(Order order);
    }
    public interface IOrderReporting
    {
        void SendNotification(Order order);
        void GenerateReport(DateTime from, DateTime to);
        void ExportToExcel(string filePath);
    }
    public interface IDatabaseAdmin
    {
        void BackupDatabase();
        void RestoreDatabase();
    }
    public class CustomerPortal : IOrderBaseActions
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine("Order created by customer");
        }
        public void UpdateOrder(Order order) 
        {
            Console.WriteLine("Order updated by customer");
        }
        public void DeleteOrder(int orderId) 
        {
            Console.WriteLine($"Order {orderId} deleted by customer");
        }
    }
    public class OrderManager : IOrderBaseActions, IOrderManagement, IOrderReporting, IDatabaseAdmin
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine("Order created");
        }
        public void UpdateOrder(Order order) 
        {
            Console.WriteLine("Order updated");
        }
        public void DeleteOrder(int orderId) 
        {
            Console.WriteLine($"Order {orderId} deleted");
        }
        public void ProcessPayment(Order order)
        {
            Console.WriteLine("Payment processed");
        }
        public void ShipOrder(Order order)
        {
            Console.WriteLine("Order shipped");
        }
        public void GenerateInvoice(Order order)
        {
            Console.WriteLine("Invoice generated");
        }
        public void SendNotification(Order order)
        {
            Console.WriteLine("Notification sent");
        }
        public void GenerateReport(DateTime from, DateTime to)
        {
            Console.WriteLine($"Report from {from} to {to} generated");
        }
        public void ExportToExcel(string filePath)
        {
            Console.WriteLine($"Exported to Excel: {filePath}");
        }
        public void BackupDatabase()
        {
            Console.WriteLine("Database backed up");
        }
        public void RestoreDatabase()
        {
            Console.WriteLine("Database restored");
        }
    }
}