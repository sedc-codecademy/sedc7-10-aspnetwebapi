using DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : IRepository<DtoUser>
    {
        private readonly PhonebookDbContext _db;
        public UserRepository(PhonebookDbContext context)
        {
            _db = context;
        }

        public IEnumerable<DtoUser> GetAll()
        {
            return _db.Users.Include(u => u.Contacts).ToList();
        }

        public void Add(DtoUser entity)
        {
            _db.Users.Add(entity);

            _db.SaveChanges();
        }
        
        public void Update(DtoUser entity)
        {
            DtoUser user = _db.Users.
                SingleOrDefault(u => u.Id == entity.Id);

            user.Username = entity.Username;
            user.Password = entity.Password;
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;

            _db.SaveChanges();
        }

        public void Delete(DtoUser entity)
        {
            _db.Users.Remove(entity);

            _db.SaveChanges();
        }

    }
}
