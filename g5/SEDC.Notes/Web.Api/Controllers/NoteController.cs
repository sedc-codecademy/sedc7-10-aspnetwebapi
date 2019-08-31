using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Serilog;
using Services;
using Services.Exceptions;

namespace Web.Api.Controllers
{
    [Authorize]
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
        public ActionResult<IEnumerable<NoteModel>> Get()
        {
            try
            {
                var userId = GetAuthorizedUserId();
                return Ok(_noteService.GetUserNotes(userId));
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}",
                    ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NoteException ex)
            {
                Log.Error("NOTE {noteId} for user {userId}: {message}",
                    ex.NoteId, ex.UserId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<NoteModel> Get(int id)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                return Ok(_noteService.GetNote(id, userId));
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}",
                    ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NoteException ex)
            {
                Log.Error("NOTE {noteId} for user {userId}: {message}",
                    ex.NoteId, ex.UserId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] NoteModel model)
        {
            try
            {
                model.UserId = GetAuthorizedUserId();
                _noteService.AddNote(model);
                return Ok("Successfully added note.");
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}",
                    ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NoteException ex)
            {
                Log.Error("NOTE {noteId} for user {userId}: {message}",
                    ex.NoteId, ex.UserId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _noteService.DeleteItem(id, userId);
                return Ok("Successfully deleted note");
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}",
                    ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NoteException ex)
            {
                Log.Error("NOTE {noteId} for user {userId}: {message}",
                    ex.NoteId, ex.UserId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new UserException(userId, name, 
                    "Name identifier claim does not exist!");
            }
            return userId;
        }
    }
}