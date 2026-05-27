using lab9.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9.Services
{
    public interface IContactService
    {
        ObservableCollection<Contact> Contacts { get; }
        void AddContact(Contact contact);
        void RemoveContact(Contact contact);
        void UpdateContact(Contact oldContact, Contact newContact);
    }
}
