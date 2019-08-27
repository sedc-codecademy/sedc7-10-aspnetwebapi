using BooksModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.ViewModels
{
    public class BookListViewModel
    {
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// The title of the book
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The name of the author of the book
        /// </summary>
        [Required]
        public string Author { get; set; }

        public int AuthorId { get; set; }

        [DefaultValue(false)]
        public bool IsRead { get; set; }

        public static BookListViewModel FromBook(Book book)
        {
            return new BookListViewModel
            {
                ID = book.ID,
                Title = book.Title,
                IsRead = book.IsRead,
                Author = book.Author.Name,
                AuthorId = book.AuthorID
            };
        }

    }
}
