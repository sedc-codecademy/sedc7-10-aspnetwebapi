using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Class2App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        public static List<Cinema> _cinemas = new List<Cinema>()
        {
            new Cinema(1, "Cineplexx", "City Mall Skopje"),
            new Cinema(2, "Milenium", "GTC Skopje")
        };

        // GET: api/Cinema
        [HttpGet]
        public IEnumerable<Cinema> Get()
        {
            return _cinemas;
        }

        // GET: api/Cinema/5
        [HttpGet("{id}")]
        public Cinema Get(int id)
        {
            return _cinemas.FirstOrDefault(x => x.Id == id);
        }

        // POST: api/Cinema
        [HttpPost]
        public void Post([FromBody] Cinema cinema)
        {
            _cinemas.Add(cinema);
        }

        [Route("name/{name}/address/{address}")]
        [HttpGet]
        public IEnumerable<Cinema> Filter(string name, string address)
        {
            return _cinemas.Where(x => x.Name.ToLower().Contains(name) && x.Address.ToLower().Contains(address));
        }

        [Route("header")]
        [HttpGet]
        public string HeaderData([FromHeader(Name = "User-Agent")] string name)
        {
            return $"You are using {name}";
        }

        //[Route("filterquery")]
        //[HttpGet]
        //public IEnumerable<Cinema> FilterQuery(string name, string address)
        //{
        //    name = name ?? string.Empty;
        //    return _cinemas.Where(x => x.Name.ToLower().Contains(name) && x.Address.ToLower().Contains(address));
        //}

        [Route("filterquery")]
        [HttpGet]
        public IEnumerable<Cinema> FilterQuery([FromQuery]FilterModel filter)
        {
            filter.Name = filter.Name ?? string.Empty;
            return _cinemas.Where(x => x.Name.ToLower().Contains(filter.Name) && x.Address.ToLower().Contains(filter.Address));
        }

        //// PUT: api/Cinema/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
