using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ToDoApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET: api/todo
        [Route("all")]
        [HttpGet]
        public IEnumerable<ToDoItemModel> Get()
        {
            var userId = GetUserId();

            return _toDoService.GetAllByUser(userId);
        }

        // POST: api/ToDo
        [HttpPost]
        public void Post([FromBody] ToDoItemModel model)
        {
            var userId = GetUserId();
            _toDoService.AddTodoItem(userId, model);
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ToDoItemModel model)
        {
            var userId = GetUserId();
            model.Id = id;
            _toDoService.UpdateTodoItem(userId, model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var userId = GetUserId();

            _toDoService.DeleteTodoItem(userId, id);
        }

        private int GetUserId()
        {
            if(!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out var userId)) 
                throw new Exception("Invalid user Id");

            return userId;
        }
    }
}
