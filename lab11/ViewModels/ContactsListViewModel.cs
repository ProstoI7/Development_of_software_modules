using lab9.Models;
using lab9.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        private readonly IContactService _contactService;
        public ObservableCollection<Contact> Contacts => _contactService.Contacts;

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

        public ContactsListViewModel(INavigationService navigation, IDialogService dialogService, IContactService contactService)
        {
            _dialogService = dialogService ?? throw new System.ArgumentNullException(nameof(dialogService));
            _navigation = navigation;
            EditContactCommand = new RelayCommand(
                () => _navigation.NavigateTo<ContactEditViewModel>(SelectedContact),
                () => SelectedContact != null
            );
            //Contacts = new ObservableCollection<Contact>();
            _contactService = contactService;

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            if (Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning("Контакт с таким номером телефона уже существует!");
                return;
            }

            try
            {
                var newContact = new Contact(Name, Phone);

                //Contacts.Add(newContact);
                _contactService.AddContact(newContact);

                _dialogService.ShowInfo($"Контакт {Name} успешно добавлен.");

                Name = string.Empty;
                Phone = string.Empty;
            }
            catch (ArgumentException ex)
            {
                _dialogService.ShowWarning(ex.Message, "Ошибка валидации");
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        private void DeleteContact()
        {
            if (SelectedContact != null)
            {
                bool isConfirmed = _dialogService.ShowConfirmation($"Вы уверены, что хотите удалить контакт {SelectedContact.Name}?");
                if (isConfirmed)
                {
                    //Contacts.Remove(SelectedContact);
                    _contactService.RemoveContact(SelectedContact);
                }
            }
        }

        private bool CanDeleteContact()
        {
            return SelectedContact != null;
        }
    }
}
