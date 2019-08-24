using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksModels
{
    [Table("Novels")]
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public bool IsRead { get; set; }

        public virtual Author Author { get; set; }

    }
}
