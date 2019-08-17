using Facebook.WebApi2_0.Exceptions;
using Facebook.WebApi2_0.Models;
using Facebook.WebApi2_0.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.WebApi2_0.Services
{
    public class UsersService : IUsersService
    {
        private static readonly IList<User> s_users = new List<User>
        {
            new User
            {
                Birthday = new DateTime(1990, 9, 9),
                Email = "trajko@gmail.com",
                FirstName = "Trajko",
                Gender = Gender.Male,
                LastName = "Trajkov",
                Password = "123456",
                Username = "tt"
            },
            new User
            {
                Birthday = new DateTime(1995, 9, 9),
                Email = "ana@gmail.com",
                FirstName = "Ana",
                Gender = Gender.Female,
                LastName = "Angelova",
                Password = "123456",
                Username = "a.angelova"
            },
            new User
            {
                Birthday = new DateTime(1990, 9, 9),
                Email = "mile@gmail.com",
                FirstName = "Mile",
                Gender = Gender.Male,
                LastName = "Milev",
                Password = "123456",
                Username = "mmilev"
            },
        };

        public User AddUser(User user)
        {
            if (s_users.Any(u => u.Username == user.Username))
                throw new ArgumentException($"The user with username: {user.Username} already exists");

            s_users.Add(user);
            return user;
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Parameter must have value with non white characters", nameof(username));

            var user = s_users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                throw new UserNotFoundException("The user does not exist");

            return user;
        }

        public IEnumerable<User> GetUsers(DateTime? startDate, DateTime? endDate)
        {
            var users = s_users;

            if (startDate.HasValue)
                users = users.Where(u => u.Birthday >= startDate)
                            .ToList();

            if (endDate.HasValue)
                users = users.Where(u => u.Birthday <= endDate)
                            .ToList();

            return users;
        }

        public User UpdateUser(User user)
        {
            var userFromMemory = s_users.SingleOrDefault(u => u.Username == user.Username);
            if (userFromMemory == null)
                throw new UserNotFoundException($"User with username: {user.Username} does not exist");

            userFromMemory.Birthday = user.Birthday;
            userFromMemory.Email = user.Email;
            userFromMemory.FirstName = user.FirstName;
            userFromMemory.Gender = user.Gender;
            userFromMemory.LastName = user.LastName;
            userFromMemory.Password = user.Password;

            return userFromMemory;
        }
    }
}
