using System.Collections.Generic;
using System.Linq;
using DataModels;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ToDoItemRepository : IRepository<DtoToDoItem>
    {
        private readonly IConfiguration _configuration;

        public ToDoItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<DtoToDoItem> GetAll()
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                return context.ToDoList.ToList();
            }
        }

        public void Add(DtoToDoItem entity)
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                context.ToDoList.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(DtoToDoItem entity)
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                context.ToDoList.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Update(DtoToDoItem entity)
        {
            using (var context = new ToDoAppDbContext(_configuration.GetConnectionString("ToDoApp")))
            {
                context.ToDoList.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
