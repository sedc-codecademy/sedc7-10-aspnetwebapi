using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Model;
using BooksApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var repo = new BookRepository();
            var result = repo.GetAllBooks();
            return new ActionResult<IEnumerable<Book>>(result);
        }

        // GET api/books/1
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var repo = new BookRepository();
            var result = repo.GetBookById(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        // GET api/books/search/stars
        [HttpGet("search/{title}")]
        public ActionResult<IEnumerable<Book>> Get(string title)
        {
            var repo = new BookRepository();
            var result = repo.GetBooksByTitle(title);
            return new ActionResult<IEnumerable<Book>>(result);
        }


        // POST api/values
        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            var repo = new BookRepository();
            var result = repo.AddBook(book);
            return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Book> Put(int id, [FromBody] Book book)
        {
            var repo = new BookRepository();
            book.ID = id;
            var result = repo.UpdateBook(book);
            return result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var repo = new BookRepository();
            var result = repo.DeleteBook(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
