using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostmanApi.Domain;

namespace PostmanApi.Controllers
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
        // GET http://localhost:50152/api/notes
        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            return notes;
        }
        // GET http://localhost:50152/api/notes/queryId?id=1
        [HttpGet("queryId")]
        public ActionResult<Note> QueryId(int id)
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
                return BadRequest($"BROKEN: {ex.Message}");
            }
        }
        // GET http://localhost:50152/api/notes/routeId/1
        [HttpGet("routeId/{id}")]
        public ActionResult<Note> RouteId(int id)
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
                return BadRequest($"BROKEN: {ex.Message}");
            }
        }
        // POST http://localhost:50152/api/notes/query1?text=Get+Milk&color=blue
        [HttpPost("query1")]
        public IActionResult Post1(string text, string color)
        {
            Note note = new Note()
            {
                Text = text,
                Color = color
            };
            notes.Add(note);
            return Ok("Note added in 'Database'!");
        }
        // POST http://localhost:50152/api/notes/query2?text=Get+Milk&color=blue
        [HttpPost("query2")]
        public IActionResult Post2([FromQuery] Note note)
        {
            notes.Add(note);
            return Ok("Note added in 'Database'!");
        }
        // POST http://localhost:50152/api/notes/query3?
        //      text=Get+Milk&color=blue&name=work
        [HttpPost("query3")]
        public IActionResult Post3([FromQuery] Note note, 
            [FromQuery] Tag tag)
        {
            note.Tags.Add(tag);
            notes.Add(note);
            return Ok("Note added in 'Database'!");
        }
        // POST http://localhost:50152/api/notes/body
        [HttpPost("body")]
        public IActionResult Post4([FromBody] Note note)
        {
            notes.Add(note);
            return Ok("Note added in 'Database'!");
        }
        // GET http://localhost:50152/api/notes/header1
        [HttpGet("header1")]
        public IActionResult Header1([FromHeader] string host)
        {
            return Ok($"You are accessing {host}!");
        }
        // GET http://localhost:50152/api/notes/header2
        [HttpGet("header2")]
        public IActionResult Header2([FromHeader] string host,
            [FromHeader(Name = "Accept-Language")] string lang)
        {
            if(lang == "mk-MK")
            {
                return Ok($"Пристапивте до {host}!");
            }
            return Ok($"You are accessing {host}!");
        }
    }
}