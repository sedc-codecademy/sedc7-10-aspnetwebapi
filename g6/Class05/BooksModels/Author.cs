using System;
using System.Collections.Generic;

namespace BooksModels
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}