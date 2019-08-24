using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ContactRepository : IRepository<DtoContact>
    {
        private readonly PhonebookDbContext _db;
        public ContactRepository(PhonebookDbContext context)
        {
            _db = context;
        }

        public IEnumerable<DtoContact> GetAll()
        {
            return _db.Contacts.ToList();
        }

        public void Add(DtoContact entity)
        {
            _db.Contacts.Add(entity);

            _db.SaveChanges();
        }


        public void Update(DtoContact entity)
        {
            var contact = _db.Contacts
                .SingleOrDefault(c => c.Id == entity.Id);

            contact.FirstName = entity.FirstName;
            contact.LastName = entity.LastName;
            contact.Email = entity.Email;
            contact.Address = entity.Address;
            contact.PhoneNumber = entity.PhoneNumber;

            _db.SaveChanges();
        }

        public void Delete(DtoContact entity)
        {
            _db.Contacts.Remove(entity);

            _db.SaveChanges();
        }
    }
}
