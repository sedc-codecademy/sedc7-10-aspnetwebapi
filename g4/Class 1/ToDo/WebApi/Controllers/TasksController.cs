using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using models = WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static IList<models.Task> _tasks = new List<models.Task>
            {
                new models.Task
                {
                    Id = 1,
                    Title = "Task 1"
                },
                new models.Task
                {
                    Id = 2,
                    Title = "Task 2"
                },
                new models.Task
                {
                    Id = 3,
                    Title = "Task 3"
                }
            };

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<models.Task> Get()
        {
            return _tasks;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<models.Task> Get(int id)
        {
            var task = _tasks.SingleOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            return task;
        }

        // POST: api/Tasks
        [HttpPost]
        public IActionResult Post([FromBody] models.Task task)
        {
            var maxId = _tasks.Max(t => t.Id);
            task.Id = maxId + 1;
            _tasks.Add(task);

            return Created($@"http://localhost:60430/api/tasks/{task.Id}", task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] models.Task task)
        {
            var taskFromMemory = _tasks.SingleOrDefault(t => t.Id == id);

            if (taskFromMemory == null)
                return NotFound();

            taskFromMemory.Title = task.Title;
            return Ok(taskFromMemory);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskForDelete = _tasks.SingleOrDefault(t => t.Id == id);
            if (taskForDelete == null)
                return NotFound();

            _tasks.Remove(taskForDelete);

            return NoContent();
        }
    }
}
