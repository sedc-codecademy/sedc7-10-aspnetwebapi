using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test01.Models
{
    public class Author
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
