using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IContactService
    {
        IEnumerable<ContactModel> GetUserContacts(int userId);
        ContactModel GetContact(int id, int userId);
        void AddContact(ContactModel contact, int userId);
        void UpdateContact(ContactModel contact, int userId);
        void DeleteContact(int id, int userId);
    }
}
