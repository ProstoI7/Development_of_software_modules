namespace SOLID_Fundamentals
{
    public interface IDiscountStrategy
    {
        decimal Calculate(decimal amount);
    }

    public class RegularDiscount : IDiscountStrategy
    {
        public decimal Calculate(decimal amount) => amount * 0.05m;
    }
    public class PremiumDiscount : IDiscountStrategy
    {
        public decimal Calculate(decimal amount) => amount * 0.10m;
    }
    public class VipDiscount : IDiscountStrategy
    {
        public decimal Calculate(decimal amount) => amount * 0.15m;
    }
    public class StudentDiscount : IDiscountStrategy
    {
        public decimal Calculate(decimal amount) => amount * 0.08m;
    }
    public class SeniorDiscount : IDiscountStrategy
    {
        public decimal Calculate(decimal amount) => amount * 0.07m;
    }
    public interface IShippingStrategy
    {
        decimal Calculate(decimal weight, string destination);
    }

    public class StandardShipping : IShippingStrategy
    {
        public decimal Calculate(decimal weight, string dest) => 5.00m + (weight * 0.5m);
    }
    public class ExpressShipping : IShippingStrategy
    {
        public decimal Calculate(decimal weight, string dest) => 15.00m + (weight * 1.0m);
    }
    public class OvernightShipping : IShippingStrategy
    {
        public decimal Calculate(decimal weight, string dest) => 25.00m + (weight * 2.0m);
    }
    public class InternationalShipping : IShippingStrategy
    {
        public decimal Calculate(decimal weight, string dest)
        {
            return dest switch
            {
                "USA" => 30.00m,
                "Europe" => 35.00m,
                "Asia" => 40.00m,
                _ => 50.00m
            };
        }
    }

    public class DiscountCalculator
    {
        public decimal CalculateDiscount(IDiscountStrategy strategy, decimal amount) => strategy.Calculate(amount);
        public decimal CalculateShipping(IShippingStrategy strategy, decimal weight, string dest) => strategy.Calculate(weight, dest);
    }
}