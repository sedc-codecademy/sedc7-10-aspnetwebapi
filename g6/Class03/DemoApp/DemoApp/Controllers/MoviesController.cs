using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public static List<Movie> _movies = new List<Movie>()
        {
            new Movie(1, "Rambo", 120),
            new Movie(2, "Rambo2", 128),
            new Movie(3, "Rambo3", 150),
        };

        [HttpGet]
        public List<Movie> GetAll()
        {
            return _movies;
        }

        [HttpGet("{id}")]
        public Movie GetById(int id)
        {
            return _movies.FirstOrDefault(x => x.Id == id);
        }

        [Route("byname/{name}")]
        [HttpGet]
        public Movie GetByName(string name)
        {
            return _movies.FirstOrDefault(x => x.Name == name);
        }

        [Route("name/{name}/length/{length}")]
        [HttpGet]
        public IEnumerable<Movie> GetByFilter(string name, int length)
        {
            return _movies
                .Where(x => 
                    x.Name.Contains(name) 
                    && x.Length > length)
                .ToList();
        }

        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            _movies.Add(movie);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie movie)
        {
            movie.Id = id;
            var currentMovie = _movies.FirstOrDefault(x => x.Id == id);
            var index = _movies.IndexOf(currentMovie);
            _movies[index] = movie;

            //if(string.IsNullOrEmpty(movie.Name))
            //    throw new Exception("Name is required field");

            //_movies[index].Name = movie.Name;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movie = _movies.FirstOrDefault(x => x.Id == id);
            _movies.Remove(movie);
        }

        [Route("header")]
        [HttpGet]
        public string HeaderParam([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return $"You are using: {userAgent} please switch to IE Edge";
        }

        [Route("text")]
        [HttpGet]
        public string PlainText([FromQuery] string text)
        {
            return text;
        }

        [Route("filterquery")]
        [HttpGet]
        public IEnumerable<Movie> GetMoviesByFilter([FromQuery] Movie filter)
        {
            return _movies
                .Where(x =>
                    x.Name.Contains(filter.Name)
                    && x.Length > filter.Length)
                .ToList();
        }
    }
}