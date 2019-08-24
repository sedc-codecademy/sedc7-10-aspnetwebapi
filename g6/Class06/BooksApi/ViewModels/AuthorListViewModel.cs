using BooksModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.ViewModels
{
    public class AuthorListViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public int BookCount { get; set; }

        public static AuthorListViewModel FromAuthor(Author author)
        {
            return new AuthorListViewModel
            {
                ID = author.ID,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath,
                BookCount = author.Books.Count()
            };
        }
    }
}
