using Facebook.WebApi2_0.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Facebook.WebApi2_0.Models
{
    public class User
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender? Gender { get; set; }

        public static User FromViewModel(UpdateUserViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new User
            {
                Birthday = viewModel.Birthday,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                Gender = viewModel.Gender,
                LastName = viewModel.LastName,
                Password = viewModel.Password
            };
        }
    }
}
