using OOP_Fundamentals_library;

namespace OOP_Fundamentals_Library
{
    public class PayrollSystem
    {
        public void ProcessSalary(IPayable employee)
        {
            employee.ProcessSalary();
        }

        public decimal CalculateBonus(IPayable employee, int years, bool hasCertification)
        {
            return employee.CalculateBonus(years, hasCertification);
        }
    }
}
