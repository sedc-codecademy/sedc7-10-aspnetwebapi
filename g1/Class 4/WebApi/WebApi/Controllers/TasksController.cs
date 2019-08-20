using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using Task = WebApi.Models.Task;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static IList<Task> _tasks = new List<Task>
        {
            new Task
            {
                Id = 1,
                Title = "Task 1",
                Description = "Description of Task 1"
            },
            new Task
            {
                Id = 2,
                Title = "Task 2",
                Description = "Description of Task 2"
            },
            new Task
            {
                Id = 3,
                Title = "Task 3",
                Description = "Description of Task 3"
            },
            new Task
            {
                Id = 4,
                Title = "Task 4",
                Description = "Description of Task 4"
            }
        };

        private readonly TaskRepository repo;

        public TasksController(TaskRepository _repo)
        {
            repo = _repo;
        }
        // GET api/tasks
        [HttpGet]
        public ActionResult<List<Task>> Get()
        {
            //return StatusCode(StatusCodes.Status200OK, "Hello World");
            return Ok(repo.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            Task task = repo.GetTask(id);

            if(null == task)
            {
                return NotFound($"Task with id: {id} not found");
            }

            return Ok(task);
        }

        // api/tasks
        [HttpPost]
        public ActionResult<List<Task>> Post([FromBody] Task newTask)
        {
            List<Task> tasks = repo.CreateTask(newTask);
            
            return Ok(tasks);
        }


        [HttpPut("{id}")]
        public ActionResult<Task> Put(int id, [FromBody] Task obj)
        {
            Task updatedTask = repo.UpdateTask(id, obj);

            if(null == updatedTask)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, updatedTask);
        }

    }
}