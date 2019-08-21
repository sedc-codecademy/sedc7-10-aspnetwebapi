using System.Collections.Generic;
using System.Linq;
using DataModels;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ToDoItemRepository : IRepository<DtoToDoItem>
    {
        private readonly string _connectionString;


        public ToDoItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ToDoDatabase");
        }

        public IEnumerable<DtoToDoItem> GetAll()
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.ToDoItems.ToList();
            }
        }

        public DtoToDoItem GetById(int id)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                return dbContext.ToDoItems.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Add(DtoToDoItem entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.ToDoItems.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(DtoToDoItem entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.ToDoItems.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public void Update(DtoToDoItem entity)
        {
            using (var dbContext = new ToDoAppContext(_connectionString))
            {
                dbContext.ToDoItems.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
