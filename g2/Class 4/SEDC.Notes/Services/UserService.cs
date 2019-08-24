using DataAccess;
using DataModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserDto> _userRepository;
        public UserService(IRepository<UserDto> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel Authenticate(string username, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(
                Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = _userRepository.GetAll()
                .SingleOrDefault(x => x.Username == username &&
                x.Password == hashedPassword);

            if (user == null) return null;

            // mapping
            UserModel userModel = new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName, 
                Username = user.Username
            };

            return userModel;
        }

        public void RegisterUser(RegisterModel model)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(
                Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            // mapping
            UserDto user = new UserDto()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = hashedPassword
            };

            _userRepository.Add(user);
        }
    }
}
