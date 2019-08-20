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

        public Task GetTask(int id)
        {
            return db.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public List<Task> CreateTask(Task newTask)
        {
            db.Tasks.Add(newTask);
            db.SaveChanges();
            return GetAll();
        }

        public Task UpdateTask(int id, Task updatedTask)
        {
            Task task = GetTask(id);
            if(null == task)
            {
                return null;
            }
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsComplete = updatedTask.IsComplete;

            //db.Tasks.Update(task);
            db.SaveChanges();

            return GetTask(id);
        }
    }
}
