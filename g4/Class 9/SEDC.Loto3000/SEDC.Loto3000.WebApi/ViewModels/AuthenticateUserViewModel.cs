using System.ComponentModel.DataAnnotations;

namespace SEDC.Loto3000.WebApi.ViewModels
{
    public class AuthenticateUserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
