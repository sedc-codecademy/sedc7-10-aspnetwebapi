using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModels;
using Models;
using Repository;

namespace Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<DtoContact> _contactRepo;
        public ContactService(IRepository<DtoContact> contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public IEnumerable<ContactModel> GetUserContacts()
        {
            return _contactRepo.GetAll()
                .Select(c => new ContactModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    UserId = c.UserId
                });
        }

        public ContactModel GetContact(int id)
        {
            var contact = _contactRepo.GetAll()
                .SingleOrDefault(c => c.Id == id);

            return new ContactModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                UserId = contact.UserId
            };
        }

        public void AddContact(ContactModel contact)
        {
            _contactRepo.Add(new DtoContact
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                UserId = contact.UserId
            });
        }

        public void UpdateContact(ContactModel contact)
        {
            _contactRepo.Update(new DtoContact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                UserId = contact.UserId
            });
        }

        public void DeleteContact(int id)
        {
            var contact = _contactRepo.GetAll()
                .SingleOrDefault(c => c.Id == id);

            _contactRepo.Delete(contact);
        }
    }
}
