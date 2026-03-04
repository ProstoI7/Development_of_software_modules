using OOP_Fundamentals_library;

namespace OOP_Fundamentals_Library
{
    public class Customer: Person
    {
        public void PrintInfo()
        {
            Console.WriteLine($"Customer: {Name}, {Age} years old");
        }
    }
}
