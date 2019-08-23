using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System;

namespace SEDC.Loto3000.BusinessLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IGenericRepository<User> genericRepository, IUserRepository userRepository)
        {
            _genericRepository = genericRepository;
            _userRepository = userRepository;
        }
        public User Get(string email, string password)
        {
            var user = _userRepository.GetUser(email);

            if (user.Password == password)
                return user;

            return null;
        }

        public void Register(User user)
        {
            if (UserExists(user.Email))
                throw new ArgumentException($"User with email:{user.Email} exists");

            _genericRepository.Add(user);
        }

        private bool UserExists(string email)
        {
            var user = _userRepository.GetUser(email);
            return user != null;
        }
    }
}
