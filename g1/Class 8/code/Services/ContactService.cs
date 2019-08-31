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

        public IEnumerable<ContactModel> GetUserContacts(int userId)
        {
            return _contactRepo.GetAll()
                .Where(c => c.UserId == userId)
                .Select(c => new ContactModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    Email = c.Email,
                    Phonenumber = c.PhoneNumber,
                    UserId = c.UserId
                });
        }

        public ContactModel GetContact(int id, int userId)
        {
            var contact = _contactRepo.GetAll()
                .SingleOrDefault(c => c.Id == id && c.UserId == userId);

            return new ContactModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address,
                Email = contact.Email,
                Phonenumber = contact.PhoneNumber,
                UserId = contact.UserId
            };
        }

        public void AddContact(ContactModel contact, int userId)
        {
            _contactRepo.Add(new DtoContact
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address,
                Email = contact.Email,
                PhoneNumber = contact.Phonenumber,
                UserId = userId
            });
        }

        public void UpdateContact(ContactModel contact, int userId)
        {
            _contactRepo.Update(new DtoContact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address,
                Email = contact.Email,
                PhoneNumber = contact.Phonenumber,
                UserId = userId
            });
        }

        public void DeleteContact(int id, int userId)
        {
            var contact = _contactRepo.GetAll()
                .SingleOrDefault(c => c.Id == id && c.UserId == userId);

            _contactRepo.Delete(contact);
        }
    }
}
