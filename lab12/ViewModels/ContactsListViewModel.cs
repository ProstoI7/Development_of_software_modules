using lab9.Models;
using lab9.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace lab9.ViewModels
{
    /// <summary>
    /// Главная ViewModel приложения — посредник между Model (Contact) и View (MainWindow).
    /// Содержит логику представления: коллекцию контактов, команды добавления/удаления.
    /// </summary>
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigation;
        private readonly PhoneBookDbЛуканина2307б2Context _context;

        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set => Set(ref _contacts, value);
        }


        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand EditContactCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ContactsListViewModel(INavigationService navigation, IDialogService dialogService, PhoneBookDbЛуканина2307б2Context context)
        {
            _dialogService = dialogService ?? throw new System.ArgumentNullException(nameof(dialogService));
            _navigation = navigation;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            EditContactCommand = new RelayCommand(
                () => _navigation.NavigateTo<ContactEditViewModel>(SelectedContact),
                () => SelectedContact != null
            );
            Contacts = new ObservableCollection<Contact>(_context.Contacts.ToList());

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            if (_context.Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning("Контакт с таким номером телефона уже существует!");
                return;
            }

            try
            {
                if (!ValidateContact(Name, Phone))
                {
                    _dialogService.ShowWarning("Некорректные данные контакта. Имя не должно быть пустым, а телефон должен быть в формате +7XXXXXXXXXX или от 6 до 15 цифр.", "Ошибка валидации");
                    return;
                }
                var newContact = new Contact
                {
                    Name = Name,
                    Phone = Phone
                };
                _context.Contacts.Add(newContact);
                _context.SaveChanges();
                Contacts.Add(newContact);

                _dialogService.ShowInfo($"Контакт {Name} успешно добавлен.");
                Name = string.Empty;
                Phone = string.Empty;
            }
            catch (ArgumentException ex)
            {
                _dialogService.ShowWarning(ex.Message, "Ошибка валидации");
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Ошибка при работе с базой данных: {ex.Message}");
            }
        }
        private bool ValidateContact(string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (string.IsNullOrWhiteSpace(phone)) return false;

            string pattern = @"^(\+7\d{10}|\d{6,15})$";
            string cleanPhone = phone.Replace("-", "").Replace(" ", "");
            return Regex.IsMatch(cleanPhone, pattern);
        }
        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }
        private void DeleteContact()
        {
            if (SelectedContact != null)
            {
                bool isConfirmed = _dialogService.ShowConfirmation(
                    $"Вы уверены, что хотите удалить контакт {SelectedContact.Name}?");

                if (isConfirmed)
                {
                    try
                    {
                        _context.Contacts.Remove(SelectedContact);
                        _context.SaveChanges();
                        Contacts.Remove(SelectedContact);
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowError($"Ошибка при удалении из базы данных: {ex.Message}");
                    }
                }
            }
        }

        private bool CanDeleteContact()
        {
            return SelectedContact != null;
        }
    }
}