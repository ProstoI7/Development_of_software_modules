using lab9.Models;
using lab9.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab9.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IContactService _contactService;
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

        public ContactEditViewModel(INavigationService navigation, IContactService contactService)
        {
            _navigation = navigation;
            _contactService = contactService;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save()
        {
            if (_originalContact != null)
            {
                var updatedContact = new Contact(EditName, EditPhone);

                _contactService.UpdateContact(_originalContact, updatedContact);
            }

            _navigation.NavigateTo<ContactsListViewModel>();
        }

        private void Cancel()
        {
            _navigation.NavigateTo<ContactsListViewModel>();
        }

        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact c)
            {
                _originalContact = c;
                EditName = c.Name;
                EditPhone = c.Phone;
            }
        }
    }
}

