using lab9.Models;
using lab9.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace lab9.ViewModels
{
    /// <summary>
    /// ViewModel для экрана редактирования контакта
    /// Реализует паттерн безопасной работы с Detached-сущностями "Найти-Изменить-Сохранить" по
    /// </summary>
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IDialogService _dialogService;
        private readonly IDbContextFactory<PhoneBookDbЛуканина2307б2Context> _contextFactory;

        private Contact? _originalContact;

        private string _editName = string.Empty;
        public string EditName
        {
            get => _editName;
            set => Set(ref _editName, value);
        }

        private string _editPhone = string.Empty;
        public string EditPhone
        {
            get => _editPhone;
            set => Set(ref _editPhone, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(
            INavigationService navigation,
            IDialogService dialogService,
            IDbContextFactory<PhoneBookDbЛуканина2307б2Context> contextFactory)
        {
            _navigation = navigation;
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save()
        {
            if (_originalContact == null) return;

            if (!ValidateContact(EditName, EditPhone))
            {
                _dialogService.ShowWarning("Некорректные данные контакта. Имя не должно быть пустым, а телефон должен быть в формате +7XXXXXXXXXX или от 6 до 15 цифр.", "Ошибка валидации");
                return;
            }

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    if (context.Contacts.Any(c => c.Phone == EditPhone && c.Id != _originalContact.Id))
                    {
                        _dialogService.ShowWarning("Контакт с таким номером телефона уже существует!");
                        return;
                    }

                    var dbContact = context.Contacts.Find(_originalContact.Id);
                    if (dbContact != null)
                    {
                        dbContact.Name = EditName;
                        dbContact.Phone = EditPhone;
                        context.SaveChanges();
                        _dialogService.ShowInfo("Изменения успешно сохранены.");
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Ошибка при сохранении изменений в базу данных: {ex.Message}");
                return;
            }

            _navigation.NavigateTo<ContactsListViewModel>();
        }

        private void Cancel() => _navigation.NavigateTo<ContactsListViewModel>();

        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact c)
            {
                _originalContact = c;
                EditName = c.Name;
                EditPhone = c.Phone;
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
    }
}