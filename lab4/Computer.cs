using System;
using System.Collections.Generic;

namespace Lab4
{
    public class Computer
    {
        public string CPU { get; set; }
        public int RAM { get; set; }
        public string GPU { get; set; }
        public List<string> AdditionalComponents { get; set; } = new List<string>();

        public void Display()
        {
            Console.WriteLine($"CPU: {CPU} | RAM: {RAM} ГБ | GPU: {GPU}");
            Console.WriteLine($"Доп. компоненты: {(AdditionalComponents.Count > 0 ? string.Join(", ", AdditionalComponents) : "Нет")}");
        }

        public Computer ShallowCopy()
        {
            return (Computer)this.MemberwiseClone();
        }

        public Computer DeepCopy()
        {
            Computer copy = (Computer)this.MemberwiseClone();
            copy.AdditionalComponents = new List<string>(this.AdditionalComponents);
            return copy;
        }
    }
}