using Facebook.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Facebook.WebApi.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
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
    }
}
