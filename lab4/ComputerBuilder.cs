namespace Lab4
{
    public class ComputerBuilder
    {
        private Computer _computer = new Computer();

        public ComputerBuilder WithCPU(string cpu)
        {
            _computer.CPU = cpu;
            return this;
        }
        public ComputerBuilder WithRAM(int ram)
        {
            _computer.RAM = ram;
            return this;
        }
        public ComputerBuilder WithGPU(string gpu)
        {
            _computer.GPU = gpu;
            return this;
        }
        public ComputerBuilder WithComponent(string component)
        {
            _computer.AdditionalComponents.Add(component);
            return this;
        }
        public Computer Build()
        {
            return _computer;
        }
    }

    public static class ComputerFactories
    {
        public static Computer CreateOfficeComputer()
        {
            return new ComputerBuilder()
                .WithCPU("Intel Core i3")
                .WithRAM(8)
                .WithGPU("Intel UHD Graphics")
                .WithComponent("Standard Cooler")
                .Build();
        }

        public static Computer CreateGamingComputer()
        {
            return new ComputerBuilder()
                .WithCPU("AMD Ryzen 7")
                .WithRAM(32)
                .WithGPU("NVIDIA RTX 4080")
                .WithComponent("Liquid Cooling")
                .WithComponent("1TB NVMe SSD")
                .Build();
        }

        public static Computer CreateHomeComputer()
        {
            return new ComputerBuilder()
                .WithCPU("Intel Core i5")
                .WithRAM(16)
                .WithGPU("NVIDIA GTX 1660")
                .WithComponent("512GB SSD")
                .Build();
        }
    }
}