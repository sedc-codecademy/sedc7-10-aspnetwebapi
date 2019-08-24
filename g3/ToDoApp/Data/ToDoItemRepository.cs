using System.Collections.Generic;
using System.Linq;
using DataModels;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ToDoItemRepository : IRepository<ToDoItem>
    {
        private readonly string _connectionString;


        public ToDoItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ToDoDatabase");
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.ToDoItems.ToList();
            }
        }

        public ToDoItem GetById(int id)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.ToDoItems.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Add(ToDoItem entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.ToDoItems.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(ToDoItem entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.ToDoItems.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public void Update(ToDoItem entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.ToDoItems.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
