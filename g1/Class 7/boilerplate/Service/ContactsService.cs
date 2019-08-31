using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class ContactsService
    {
        private readonly ContactsRepository _repo;

        public ContactsService(ContactsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _repo.GetAllContacts()
                .Select(c => new Contact
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.Lastname,
                    Email = c.Email,
                    Address = c.Address,
                    PhoneNumber = c.Phonenumber,
                    UserId = c.UserId
                });
        }
    }
}
