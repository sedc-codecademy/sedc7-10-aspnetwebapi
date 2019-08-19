using Facebook.WebApi2_0.Models;
using Facebook.WebApi2_0.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.WebApi2_0.Repositories
{
    public class InMemoryUsersRepository : IUsersRepository
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
            s_users.Add(user);
            return user;
        }

        public void DeleteUser(string username)
        {
            var user = GetByUsername(username);
            s_users.Remove(user);
        }

        public User GetByUsername(string username)
        {
            return s_users.SingleOrDefault(u =>  u.Username == username);
        }

        public IEnumerable<User> GetUsers(DateTime? startDate = null, DateTime? endDate = null)
        {
            var users = s_users;

            if (startDate.HasValue)
                users = users.Where(u => u.Birthday >= startDate)
                            .ToList();

            if (endDate.HasValue)
                users = users.Where(u => u.Birthday <= endDate)
                            .ToList();

            return s_users;
        }

        public User UpdateUser(User user)
        {
            var dbUser = GetByUsername(user.Username);
            //as temporary solution
            dbUser.FirstName = user.FirstName;

            return dbUser;
        }
    }
}
