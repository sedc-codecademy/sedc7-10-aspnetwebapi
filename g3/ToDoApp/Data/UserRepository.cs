using System.Collections.Generic;
using System.Linq;
using DataModels;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class UserRepository : IRepository<DtoUser>
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ToDoDatabase");
        }
        public IEnumerable<DtoUser> GetAll()
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.Users.ToList();
            }
        }

        public DtoUser GetById(int id)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Add(DtoUser entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.Users.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(DtoUser entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.Users.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public void Update(DtoUser entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.Users.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
