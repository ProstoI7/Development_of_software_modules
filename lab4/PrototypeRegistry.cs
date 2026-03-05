using System;
using System.Collections.Generic;

namespace Lab4
{
    public sealed class PrototypeRegistry
    {
        private static readonly Lazy<PrototypeRegistry> _instance = new Lazy<PrototypeRegistry>(() => new PrototypeRegistry());

        private readonly Dictionary<string, Computer> _prototypes;

        private PrototypeRegistry()
        {
            _prototypes = new Dictionary<string, Computer>
            {
                { "office", ComputerFactories.CreateOfficeComputer() },
                { "gaming", ComputerFactories.CreateGamingComputer() },
                { "home", ComputerFactories.CreateHomeComputer() }
            };
        }

        public static PrototypeRegistry Instance => _instance.Value;

        public Computer GetPrototype(string key)
        {
            if (_prototypes.TryGetValue(key.ToLower(), out Computer prototype))
            {
                return prototype.DeepCopy();
            }
            throw new ArgumentException("Такого прототипа нет в реестре.");
        }
    }
}