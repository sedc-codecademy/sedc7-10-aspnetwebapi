using System.Collections.Generic;
using System.Linq;
using DataModels;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ToDoDatabase");
        }
        public IEnumerable<User> GetAll()
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.Users.ToList();
            }
        }

        public User GetById(int id)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Add(User entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.Users.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(User entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.Users.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public void Update(User entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.Users.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
