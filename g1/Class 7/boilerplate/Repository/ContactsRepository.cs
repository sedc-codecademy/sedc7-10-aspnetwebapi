using DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository
{
    public class ContactsRepository
    {
        private readonly PhonebookDbContext _db;

        public ContactsRepository(PhonebookDbContext context)
        {
            _db = context;
        }

        public IEnumerable<DtoContact> GetAllContacts()
        {
            return _db.Contacts.ToList();
        }
    }
}
