using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SEDC.G5.Class2.Controllers
{
    public class Note
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public List<Tag> Tags { get; set; }
    }
    public class Tag
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class NoteObjectsController : ControllerBase
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

        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            return notes;
        }

        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            try
            {
                return notes[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound("Note with that Id does not exist!");
            }
            catch (Exception ex)
            {
                return BadRequest("BROKEN REQUEST - " + ex.Message);
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
            Note note = JsonConvert.DeserializeObject<Note>(body);
            notes.Add(note);
            return Ok($"Note with id {notes.Count - 1} has been added!");
        }

        [HttpGet("{id}/tags")]
        public ActionResult<List<Tag>> Tags(int id)
        {
            try
            {
                return notes[id - 1].Tags;
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound("Note with that Id does not exist!");
            }
            catch (Exception ex)
            {
                return BadRequest("BROKEN REQUEST - " + ex.Message);
            }
        }

        [HttpGet("{noteId}/tags/{tagId}")]
        public ActionResult<Tag> Tags(int noteId, int tagId)
        {
            try
            {
                return notes[noteId - 1].Tags[tagId - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound("Note or tag with that Id does not exist!");
            }
            catch (Exception ex)
            {
                return BadRequest("BROKEN REQUEST - " + ex.Message);
            }
        }
    }
}