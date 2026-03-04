using OOP_Fundamentals_library;

namespace OOP_Fundamentals_Library
{
    public class Manager : Employee
    {
        private string _department;
        private readonly List<Employee> _team = new();

        public IReadOnlyCollection<Employee> Team => _team.AsReadOnly();

        public string Department
        {
            get => _department;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Department cannot be empty");
                _department = value;
            }
        }

        public int TeamSize => _team.Count;

        public override void PrintInfo()
        {
            Console.WriteLine($"Manager: {Name}, {Age} years old, Department: {Department}");
        }

        public override void GenerateReport()
        {
            Console.WriteLine($"Manager Report:");
            Console.WriteLine($"  Name: {Name}");
            Console.WriteLine($"  Department: {Department}");
            Console.WriteLine($"  Team Size: {TeamSize}");
        }

        public override void ProcessSalary()
        {
            Console.WriteLine($"Processing salary for manager {Name}: {Salary}");
            Salary += 2000;
        }

        public override decimal CalculateBonus(int years, bool hasCertification)
        {
            decimal bonus = Salary * 0.2m;
            if (years > 5) bonus += 500;
            if (hasCertification) bonus += 300;
            return bonus;
        }

        public void AddTeamManager(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            _team.Add(employee);
        }
    }
}
