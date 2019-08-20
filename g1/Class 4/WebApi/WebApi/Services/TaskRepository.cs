using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services
{
    public class TaskRepository
    {
        private readonly ToDoDbContext db;

        public TaskRepository(ToDoDbContext context)
        {
            db = context;
        }

        public List<Task> GetAll()
        {
            return db.Tasks.ToList();
        }
    }
}
