using Facebook.WebApi2_0.Exceptions;
using Facebook.WebApi2_0.Models;
using Facebook.WebApi2_0.Repositories.Contracts;
using Facebook.WebApi2_0.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.WebApi2_0.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User AddUser(User user)
        {
            if (UserExists(user.Username))
                throw new ArgumentException($"The user with username: {user.Username} already exists");

            _usersRepository.AddUser(user);
            return user;
        }


        public void DeleteUser(string username)
        {
            if (!UserExists(username))
                throw new UserNotFoundException($"Username {username} does not exist");

            _usersRepository.DeleteUser(username);
        }

        public User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Parameter must have value with non white characters", nameof(username));

            var user = _usersRepository.GetByUsername(username);
            if (user == null)
                throw new UserNotFoundException("The user does not exist");

            return user;
        }

        public IEnumerable<User> GetUsers(DateTime? startDate, DateTime? endDate)
        {     
            return _usersRepository.GetUsers(startDate, endDate);
        }

        public User UpdateUser(User user)
        {
            if (!UserExists(user.Username))
                throw new UserNotFoundException($"User with username: {user.Username} does not exist");

            return _usersRepository.UpdateUser(user);
        }

        private bool UserExists(string username)
        {
            var dbUser = _usersRepository.GetByUsername(username);
            return dbUser != null;
        }
    }
}
