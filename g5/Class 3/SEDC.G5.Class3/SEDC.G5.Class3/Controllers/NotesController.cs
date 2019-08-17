using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.G5.Class3.Domain;

namespace SEDC.G5.Class3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public static List<Note> notes = new List<Note>()
        {
            new Note(){ Text = "Don't forget to water the plant", Color = "blue", Tags = new List<Tag>()
                {
                    new Tag(){ Name = "Home", Color = "cyan"},
                    new Tag(){ Name = "Priority Low", Color = "green"}
                }
            },
            new Note(){ Text = "Drink more Tea", Color = "blue", Tags = new List<Tag>()
            {
                    new Tag(){ Name = "Misc", Color = "orange"},
                    new Tag(){ Name = "Priority Low", Color = "green"}
                }
            },
            new Note(){ Text = "Make a break every 1h of coding", Color = "blue", Tags = new List<Tag>()
            {
                    new Tag(){ Name = "work", Color = "blue"},
                    new Tag(){ Name = "Priority Medium", Color = "yellow"}
                }
            }
        };

        // GET localhost:49404/api/notes
        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            return notes;
        }

        // GET localhost:49404/api/notes/queryid?id=1
        [HttpGet("queryid")]
        public ActionResult<Note> GetIdQuery(int id)
        {
            try
            {
                return notes[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound("No note with that id!");
            }
            catch (Exception ex)
            {
                return BadRequest($"BROKEN REQUEST: {ex.Message}");
            }
        }

        // GET localhost:49404/api/notes/1
        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            try
            {
                return notes[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound("No note with that id!");
            }
            catch (Exception ex)
            {
                return BadRequest($"BROKEN REQUEST: {ex.Message}");
            }
        }
        // POST localhost:49404/api/notes/query1?text=Get%20Milk&color=blue
        [HttpPost("query1")]
        public IActionResult Post1(string text, string color)
        {
            Note note = new Note()
            {
                Text = text,
                Color = color
            };
            notes.Add(note);
            return Ok("A note has been created!");
        }
        // POST localhost:49404/api/notes/query2?text=Get%20Milk&color=blue
        [HttpPost("query2")]
        public IActionResult Post2([FromQuery] Note note)
        {
            notes.Add(note);
            return Ok("A note has been created!");
        }
        // POST localhost:49404/api/notes/query3?text=Get%20Milk&color=blue&name=low%20priority
        [HttpPost("query3")]
        public IActionResult Post3([FromQuery] Note note, [FromQuery] Tag tag)
        {
            note.Tags = new List<Tag>() { tag };
            notes.Add(note);
            return Ok("A note has been created!");
        }
        // POST localhost:49404/api/notes/body
        [HttpPost("body")]
        public IActionResult PostBody([FromBody] Note note)
        {
            notes.Add(note);
            return Ok("A note has been created!");
        }
        // GET localhost:49404/api/notes/header1
        [HttpGet("header1")]
        public IActionResult GetHeader1([FromHeader] string host)
        {
            return Ok($"You are accessing {host}");
        }
        // GET localhost:49404/api/notes/header2
        [HttpGet("header2")]
        public IActionResult GetHeader2(
            [FromHeader(Name ="Accept-Language")] string lang,
            [FromHeader] string host)
        {
            if(lang == "mk-MK")
            {
                return Ok($"Пристапувате до {host}");
            }
            return Ok($"You are accessing {host}");
        }
    }
}