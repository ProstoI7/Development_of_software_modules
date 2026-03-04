namespace OOP_Fundamentals_library
{
    public interface IPayable
    {
        void ProcessSalary();
        decimal CalculateBonus(int years, bool hasCertification);
    }
}
