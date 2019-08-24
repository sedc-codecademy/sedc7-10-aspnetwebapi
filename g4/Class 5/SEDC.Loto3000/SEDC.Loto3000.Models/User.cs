namespace SEDC.Loto3000.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public bool IsAdmin { get; set; }
    }
}
