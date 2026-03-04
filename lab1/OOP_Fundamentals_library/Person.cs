

namespace OOP_Fundamentals_library
{
    public abstract class Person
    {
        private string _name;
        private int _age;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                _name = value;
            }
        }
        public int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Age must be > 0");
                }
                _age = value;
            }
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"Person: {Name}, {Age} years old");
        }
    }
}
