using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Token { get; set; }
        public List<ContactModel> Contacts { get; set; }

        public UserModel()
        {
            Contacts = new List<ContactModel>();
        }
    }
}
