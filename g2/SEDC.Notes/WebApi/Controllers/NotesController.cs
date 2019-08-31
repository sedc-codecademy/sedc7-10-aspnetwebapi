using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteModel>> Get()
        {
            var userId = GetAuthorizedUserId();
            return Ok(_noteService.GetUserNotes(userId));
        }

        [HttpGet("{id}")]
        public ActionResult<NoteModel> Get(int id)
        {
            var userId = GetAuthorizedUserId();
            return Ok(_noteService.GetNote(id, userId));
        }
        [HttpPost]
        public void Post([FromBody] NoteModel model)
        {
            model.UserId = GetAuthorizedUserId();
            _noteService.AddNote(model);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var userId = GetAuthorizedUserId();
            _noteService.DeleteNote(id, userId);
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userId))
            {
                throw new Exception("Name identifier claim does not exist.");
            }
            return userId;
        }
    }
}