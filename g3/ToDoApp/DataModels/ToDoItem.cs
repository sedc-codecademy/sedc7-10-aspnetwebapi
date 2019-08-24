using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public class ToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
