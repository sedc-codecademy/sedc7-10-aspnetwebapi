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
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksController(BooksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of books
        /// </summary>
        /// <param name="isRead">Whether the specific book is read or unread</param>
        /// <param name="search">Filters the books with the specific title, using partial matching (case insensitive)</param>
        /// <returns>Array of BookListViewModel entities</returns>
        [HttpGet]
        public IEnumerable<BookListViewModel> GetBooks([FromQuery] bool? isRead, [FromQuery] string search)
        {
            IEnumerable<Book> books = _context.Books.Include(b => b.Author);

            if (string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase));
            }

            if (isRead.HasValue)
            {
                books = books.Where(b => b.IsRead == isRead);
            }

            return books.Select(b => BookListViewModel.FromBook(b));
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.ID)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.ID }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.ID == id);
        }
    }
}