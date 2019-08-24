using System.ComponentModel.DataAnnotations;

namespace SEDC.Loto3000.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        public bool IsAdmin { get; set; }
    }
}
