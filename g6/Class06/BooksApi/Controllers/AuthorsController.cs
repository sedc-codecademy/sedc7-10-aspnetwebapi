using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksDataAccess;
using BooksModels;
using BooksApi.ViewModels;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BooksContext _context;

        public AuthorsController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public IEnumerable<AuthorListViewModel> GetAuthors([FromQuery]string search, [FromQuery] bool? isAlive)
        {
            Console.WriteLine($"search is {search}");
            IEnumerable<Author> authors = _context.Authors.Include(a => a.Books);

            if (!string.IsNullOrEmpty(search))
            {
                authors = authors.Where(a => a.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase));
            }

            // [sw] whether I should filter based on status among the living at all
            if (isAlive.HasValue)
            {
                authors = authors.Where(a => a.DateOfDeath.HasValue != isAlive);
            }

                
            return authors.Select(a => AuthorListViewModel.FromAuthor(a));
        }

        // GET: api/Authors
        [HttpGet("filter/bestsellers")]
        public IEnumerable<AuthorListViewModel> GetBestsellerAuthors()
        {
            IEnumerable<Author> authors = _context.Authors.Include(a => a.Books);
            authors = authors
                .Where (a => a.Books.Count() >= 3)
                .OrderByDescending(a => a.Books.Count());
            return authors.Select(a => AuthorListViewModel.FromAuthor(a));
        }


        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.ID == id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(AuthorDetailsViewModel.FromAuthor(author));
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.ID)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.ID }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.ID == id);
        }
    }
}