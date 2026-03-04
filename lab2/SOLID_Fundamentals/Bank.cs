namespace SOLID_Fundamentals
{
    public abstract class Account
    {
        public decimal Balance { get; protected set; }
        public virtual void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public virtual decimal CalculateInterest()
        {
            return Balance * 0.01m;
        }
    }

    public interface IWithdrawable
    {
        void Withdraw(decimal amount);
    }

    public class SavingsAccount : Account, IWithdrawable
    {
        public decimal MinimumBalance { get; } = 100m;

        public void Withdraw(decimal amount)
        {
            if (Balance - amount < MinimumBalance)
            {
                throw new InvalidOperationException("Cannot go below minimum balance");
            }
            Balance -= amount;
        }
    }
    public class CheckingAccount : Account, IWithdrawable
    {
        public decimal OverdraftLimit { get; } = 500m;

        public void Withdraw(decimal amount)
        {
            if (Balance - amount < -OverdraftLimit)
            {
                throw new InvalidOperationException("Overdraft limit exceeded");
            }
            Balance -= amount;
        }
    }
    public class FixedDepositAccount : Account
    {
        public DateTime MaturityDate { get; }

        public FixedDepositAccount(DateTime maturityDate)
        {
            MaturityDate = maturityDate;
        }

        public void WithdrawAfterMaturity(decimal amount)
        {
            if (DateTime.Now < MaturityDate)
            {
                throw new InvalidOperationException("Cannot withdraw before maturity date");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            Balance -= amount;
        }
        public override decimal CalculateInterest()
        {
            return Balance * 0.05m;
        }
    }

    public class Bank
    {
        public void ProcessWithdrawal(IWithdrawable account, decimal amount)
        {
            try
            {
                account.Withdraw(amount);
                Console.WriteLine($"Successfully withdrew {amount}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
            }
        }

        public void Transfer(IWithdrawable from, Account to, decimal amount)
        {
            from.Withdraw(amount);
            to.Deposit(amount);
        }
    }
}
