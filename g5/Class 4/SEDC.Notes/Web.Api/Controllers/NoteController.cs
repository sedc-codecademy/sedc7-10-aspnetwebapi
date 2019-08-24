using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteModel>> Get([FromQuery] int userId)
        {
            return Ok(_noteService.GetUserNotes(userId));
        }
        [HttpGet("{id}")]
        public ActionResult<NoteModel> Get(int id, [FromQuery] int userId)
        {
            return Ok(_noteService.GetNote(id, userId));
        }

        [HttpPost]
        public void Post([FromBody] NoteModel model)
        {
            _noteService.AddNote(model);
        }
        [HttpDelete("{id}")]
        public void Delete( int id, [FromQuery] int userId)
        {
            _noteService.DeleteItem(id, userId);
        }
    }
}