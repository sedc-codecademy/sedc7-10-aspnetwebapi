using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services;
using Services.Exceptions;

namespace ToDoApp.Controllers
{
    /// <summary>
    /// To Do List Controller
    /// </summary>
    [Authorize]
    [Route("api/todo")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;
        ILogger<UserController> _logger;

        public ToDoItemController(IToDoItemService toDoItemService, ILogger<UserController> logger)
        {
            _toDoItemService = toDoItemService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoModel>> Get()
        {
            var userId = GetAuthorizedUserId();

            return Ok(_toDoItemService.GetUserToDoItems(userId));
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoModel> Get(int id)
        {
            var userId = GetAuthorizedUserId();

            return Ok(_toDoItemService.GetToDoItem(id, userId));
        }

        [HttpPost]
        public void Post([FromBody] ToDoModel model)
        {
            model.UserId = GetAuthorizedUserId();

            _toDoItemService.AddToDoItem(model);
        }

        [HttpPut("{id}")]
        public void Post(int id)
        {
            var userId = GetAuthorizedUserId();

            _toDoItemService.ChangeCompletenessStatus(id, userId);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var userId = GetAuthorizedUserId();

            _toDoItemService.DeleteItem(id, userId);
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                throw new ToDoException("Name Identifier claim does not exist.");
            }

            return userId;
        }
    }
}