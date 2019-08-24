using BooksModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public class AuthorDetailsBookViewModel
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public bool IsRead { get; set; }

            public static AuthorDetailsBookViewModel FromBook(Book book)
            {
                return new AuthorDetailsBookViewModel
                {
                    ID = book.ID,
                    Title = book.Title,
                    IsRead = book.IsRead
                };
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public AuthorDetailsBookViewModel[] Books { get; set; }

        public static AuthorDetailsViewModel FromAuthor(Author author)
        {
            return new AuthorDetailsViewModel
            {
                ID = author.ID,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath,
                Books = author.Books.Select(b => AuthorDetailsBookViewModel.FromBook(b)).ToArray()
            };
        }
    }




}
