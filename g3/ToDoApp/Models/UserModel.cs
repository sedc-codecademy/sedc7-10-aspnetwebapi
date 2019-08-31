using System.Collections.Generic;

namespace Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Token { get; set; }
        public List<ToDoModel> ToDoList { get; set; }

        public UserModel()
        {
            ToDoList = new List<ToDoModel>();
        }
    }
}
