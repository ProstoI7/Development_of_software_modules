using OOP_Fundamentals_library;

namespace OOP_Fundamentals_Library
{
    public class Employee : Person, IReport, IPayable
    {
        public decimal _salary;
        private string _position;

        public decimal Salary
        {
            get => _salary;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Salary cannot be negative");
                }
                _salary = value;
            }
        }
        public string Position
        {
            get => _position;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Position cannot be empty");
                }
                _position = value;
            }
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Employee: {Name}, {Age} years old");
        }
        public virtual void GenerateReport()
        {
            Console.WriteLine($"Employee Report:");
            Console.WriteLine($"  Name: {Name}");
            Console.WriteLine($"  Age: {Age}");
            Console.WriteLine($"  Salary: {Salary}");
        }

        public virtual void ProcessSalary()
        {
            Console.WriteLine($"Processing salary for employee {Name}: {Salary}");
            Salary += 1000;
        }

        public virtual decimal CalculateBonus(int years, bool hasCertification)
        {
            decimal bonus = Salary * 0.1m;
            if (years > 5) bonus += 500;
            if (hasCertification) bonus += 300;
            return bonus;
        }
    }
}
