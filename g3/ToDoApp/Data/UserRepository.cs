using System.Collections.Generic;
using System.Linq;
using DataModels;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class UserRepository : IRepository<DtoUser>
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<DtoUser> GetAll()
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                return context.Users.ToList();
            }
        }

        public void Add(DtoUser entity)
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                context.Users.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(DtoUser entity)
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                context.Users.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Update(DtoUser entity)
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                context.Users.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
