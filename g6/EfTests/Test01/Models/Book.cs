using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test01.Models
{
    [Table("Novels")]
    public class Book
    {
        [Key]
        public int ID { get; set; }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public virtual Author Author { get; set; }

        public bool IsRead { get; set; }

    }
}