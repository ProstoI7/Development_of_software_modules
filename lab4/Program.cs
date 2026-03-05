using System;

namespace Lab4
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Создание через строителя:");
            Computer customComputer = new ComputerBuilder()
                .WithCPU("Intel Core i9")
                .WithRAM(64)
                .WithGPU("NVIDIA RTX 4090")
                .WithComponent("RGB Lighting")
                .WithComponent("Capture Card")
                .Build();
            customComputer.Display();
            Console.WriteLine();

            Console.WriteLine("Копирование:");
            Computer originalOffice = ComputerFactories.CreateOfficeComputer();
            Console.WriteLine("Оригинал:");
            originalOffice.Display();
            Computer shallowClone = originalOffice.ShallowCopy();
            Computer deepClone = originalOffice.DeepCopy();

            shallowClone.AdditionalComponents.Add("Wi-Fi Adapter (Из Shallow)");
            Console.WriteLine("\nПосле поверхностного копирования:");
            Console.WriteLine("Оригинал:");
            originalOffice.Display();
            Console.WriteLine("Клон:");
            shallowClone.Display();

            deepClone.AdditionalComponents.Add("Bluetooth (Из Deep)");
            Console.WriteLine("\nПосле глубокого копирования:");
            Console.WriteLine("Оригинал:");
            originalOffice.Display();
            Console.WriteLine("Глубокий клон:");
            deepClone.Display();
            Console.WriteLine();

            Console.WriteLine("Одиночка:");
            PrototypeRegistry registry1 = PrototypeRegistry.Instance;
            PrototypeRegistry registry2 = PrototypeRegistry.Instance;
            bool isSame = object.ReferenceEquals(registry1, registry2);
            Console.WriteLine($"registry1 и registry2 — один и тот же объект в памяти? {isSame}");
            Console.WriteLine();

            Console.WriteLine("Реестр прототипов:");
            Computer myGamingPc = registry1.GetPrototype("gaming");
            Console.WriteLine("Получен прототип 'gaming' из реестра:");
            myGamingPc.Display();

            Console.WriteLine("\nМодифицируем полученный прототип (добавили оперативку и звуковую карту)...");
            myGamingPc.RAM = 128;
            myGamingPc.AdditionalComponents.Add("Hi-Fi Audio Card");
            myGamingPc.Display();

            Console.WriteLine("\nЗапрашиваем прототип 'gaming' из реестра повторно:");
            Computer pureGamingPc = registry1.GetPrototype("gaming");
            pureGamingPc.Display();
            Console.ReadLine();
        }
    }
}