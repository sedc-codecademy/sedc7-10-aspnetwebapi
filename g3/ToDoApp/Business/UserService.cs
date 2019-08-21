using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Data;
using DataModels;
using Models;

namespace Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<DtoUser> _userRepository;

        public UserService(IRepository<DtoUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(UserModel model)
        {
            if (string.IsNullOrEmpty(model.Username))
                throw new Exception("Username is required.");

            if (string.IsNullOrEmpty(model.FirstName))
                throw new Exception("FirstName is required.");

            if (string.IsNullOrEmpty(model.LastName))
                throw new Exception("LastName is required.");

            if (string.IsNullOrEmpty(model.Password))
                throw new Exception("Password is required.");

            if (_userRepository
                .GetAll()
                .Any(x => x.Username == model.Username))
                throw new Exception("The username is already in use.");
            
            if(!ValidatePassword(model.Password))
                throw new Exception("Please enter strong password.");

            if(model.Password != model.ConfirmPassword)
                throw new Exception("Password and Confirm Password are different.");

            var user = new DtoUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username
            };

            _userRepository.Add(user);
        }

        public IEnumerable<UserModel> GetAll()
        {
            return _userRepository.GetAll().Select(x => new UserModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.Username
            });
        }

        public bool ValidatePassword(string password)
        {
            var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var match = regex.Match(password);
            return match.Success;
        }
    }
}
