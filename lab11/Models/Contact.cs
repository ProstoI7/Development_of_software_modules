using lab9.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab9.Models
{
    // Model: содержит бизнес-данные и бизнес-логику
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;

            if (!Validate())
            {
                throw new ArgumentException("Некорректные данные контакта.");
            }
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        // Валидация
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) return false;
            if (string.IsNullOrWhiteSpace(Phone)) return false;

            string pattern = @"^(\+7\d{10}|\d{6,15})$";
            string cleanPhone = Phone.Replace("-", "").Replace(" ", "");
            return Regex.IsMatch(cleanPhone, pattern);
        }
    }
}
