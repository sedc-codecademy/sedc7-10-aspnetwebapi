using BooksApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Service
{
    public class BookRepository
    {
        private static readonly List<Book> books = new List<Book>
        {
            new Book {ID = 1, Title = "The Calculating Stars", Author = "Mary Robinette Kowal", PublicationYear = 2018},
            new Book {ID = 2, Title = "Hitchikers Guide to the Galaxy", Author = "Douglas Adams", PublicationYear = 1979},
            new Book {ID = 3, Title = "The Man in the Rain", Author = "John Grisham", PublicationYear = 1982},
            new Book {ID = 4, Title = "The Racketeer", Author = "John Grisham", PublicationYear = 2012},
            new Book {ID = 5, Title = "A Maze of Stars", Author = "John Brunner", PublicationYear = 1991},
            new Book {ID = 6, Title = "1984", Author = "George Orwell", PublicationYear = 1948},
        };

        public IEnumerable<Book> GetAllBooks()
        {
            return books;
        }

        public Book GetBookById(int id)
        {
            return books.SingleOrDefault(b => b.ID == id);
        }

        public IEnumerable<Book> GetBooksByTitle(string titleFragment)
        {
            return books.Where(b => b.Title.Contains(titleFragment, StringComparison.InvariantCultureIgnoreCase));
        }

        public Book AddBook(Book book)
        {
            var maxId = books.Max(b => b.ID) + 1;
            var newBook = new Book
            {
                ID = maxId,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear
            };
            books.Add(newBook);
            return newBook;
        }

        public Book UpdateBook(Book book)
        {
            var oldBook = books.SingleOrDefault(b => book.ID == b.ID);
            if (oldBook == null)
            {
                return null;
            }
            oldBook.Title = book.Title;
            oldBook.Author = book.Author;
            oldBook.PublicationYear = book.PublicationYear;
            return oldBook;
        }

        public Book UpdatePublicationYear(int id, int newYear)
        {
            var oldBook = books.SingleOrDefault(b => id == b.ID);
            if (oldBook == null)
            {
                return null;
            }
            oldBook.PublicationYear = newYear;
            return oldBook;
        }

        public bool DeleteBook(int id)
        {
            var oldBook = books.SingleOrDefault(b => id == b.ID);
            if (oldBook == null)
            {
                return false;
            }
            return books.Remove(oldBook);
        }
    }
}
