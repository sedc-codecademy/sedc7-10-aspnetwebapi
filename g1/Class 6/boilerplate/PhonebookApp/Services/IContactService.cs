using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IContactService
    {
        IEnumerable<ContactModel> GetUserContacts();
        ContactModel GetContact(int id);
        void AddContact(ContactModel contact);
        void UpdateContact(ContactModel contact);
        void DeleteContact(int id);
    }
}
