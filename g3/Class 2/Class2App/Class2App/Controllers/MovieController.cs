using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Class2App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static List<Movie> _movies = new List<Movie>
        {
            new Movie("Fast&Furious", 138, "Action", 1),
            new Movie("Fast&Furious2", 158, "Action", 2),
            new Movie("Fast&Furious3", 158, "Action", 3)
        };

        public MovieController()
        {
        }

        // GET: api/Movie
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _movies;
        }

        // GET: api/Movie/5
        [Route("byId/{id}")]
        [HttpGet]
        public ActionResult<Movie> Get(int id)
        {
            var movie = _movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [Route("byName/{name}")]
        [HttpGet()]
        public Movie GetByName(string name)
        {
            return _movies.FirstOrDefault(x => x.Name == name);
        }

        // POST: api/Movie
        [HttpPost]
        public void Post([FromBody] Movie model)
        {
            _movies.Add(model);
        }

        // PUT: api/Movie/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie model)
        {
            _movies[id-1] = model;
            //_movies[id].Name = model.Name;
            //_movies[id].Length = model.Length;
            //_movies[id].Genre = model.Genre;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _movies.Remove(_movies.FirstOrDefault(x => x.Id == id));
        }

        //[Route("v1/{id}")]
        //[HttpGet]
        //public string Get(int id)
        //{
        //    return id.ToString();
        //}

        //[Route("v2/{id}")]
        //[HttpGet]
        //public string GetV2(DateTime id)
        //{
        //    return $"Id: {id.ToString(CultureInfo.InvariantCulture)}";
        //}
    }
}
