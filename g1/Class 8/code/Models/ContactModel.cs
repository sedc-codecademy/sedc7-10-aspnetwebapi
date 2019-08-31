using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public int UserId { get; set; }
    }
}
