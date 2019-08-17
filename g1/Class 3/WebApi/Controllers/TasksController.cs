using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // GET api/tasks
        [HttpGet]
        public ActionResult<IList<Task>> Get()
        {
            //return StatusCode(StatusCodes.Status200OK, "Hello World");
            return Ok(_tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            Task task = _tasks.FirstOrDefault(t => t.Id == id);

            if(null == task)
            {
                return NotFound($"Task with id: {id} not found");
            }

            return Ok(task);
        }

        // api/tasks
        [HttpPost]
        public ActionResult<Task> Post([FromBody] Task obj)
        {
            Task newTask = new Task
            {
                Id = _tasks.Count() + 1,
                Title = obj.Title,
                Description = obj.Description
            };

            _tasks.Add(newTask);

            return Ok(newTask);
        }


        [HttpPut("{id}")]
        public ActionResult<Task> Put([FromBody] Task obj, int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "I will do it I promise");
        }

    }
}