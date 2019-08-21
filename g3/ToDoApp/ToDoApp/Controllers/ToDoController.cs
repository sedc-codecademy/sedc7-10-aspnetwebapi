using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET: api/ToDo
        [Route("byUser/{id}")]
        [HttpGet]
        public IEnumerable<ToDoItemModel> Get(int id)
        {
            return _toDoService.GetAllByUser(id);
        }

        // POST: api/ToDo
        [HttpPost()]
        public void Post([FromBody] ToDoItemModel model)
        {
            _toDoService.AddTodoItem(model.UserId, model);
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ToDoItemModel model)
        {
            _toDoService.UpdateTodoItem(model.UserId, model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _toDoService.DeleteTodoItem(id);
        }
    }
}
