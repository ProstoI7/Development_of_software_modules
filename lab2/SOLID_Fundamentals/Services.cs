namespace SOLID_Fundamentals
{
    using System;

    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
    public interface ISmsService
    {
       void SendSms(string phoneNumber, string message);
    }

    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Sending email to {to}: {subject}");
        }
    }

    public class SmsService : ISmsService
    {
        public void SendSms(string phoneNumber, string message)
        {
            Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
        }
    }

    public class OrderService
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public OrderService(IEmailService emailService, ISmsService smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        public void PlaceOrder(Order order)
        {
            _emailService.SendEmail(order.CustomerEmail, "Order Confirmation", "Your order has been placed");
            _smsService.SendSms(order.CustomerPhone, "Your order has been placed");
        }
    }

    public class NotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void SendPromotion(string email, string promotion)
        {
            _emailService.SendEmail(email, "Special Promotion", promotion);
        }
    }
}
