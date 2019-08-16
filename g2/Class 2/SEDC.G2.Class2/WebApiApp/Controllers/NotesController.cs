using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public static List<string> notes = new List<string>()
        {
            "Don't forget to water plant", "Remember that you don't have a plant",
            "Buy plant from shop", "Rest after 1h of coding", "Drink Tea",
            "Don't forget to look at notes"
        };
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return notes;
        }
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return notes[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound($"Note with id {id} does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Please fix your request: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post()
        {
            string body;
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                body = sr.ReadToEnd();
            }
            notes.Add(body);
            return Ok($"A note with id {notes.Count} is created!");
        }

        [HttpGet("info")]
        public ActionResult<string> Info()
        {
            var request = Request;
            return Ok("This is an app that works with notes!");
        }
        
    }
}