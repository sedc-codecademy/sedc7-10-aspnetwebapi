using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Data;
using DataModels;
using Mappers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly JwtSettings _jwtSettings;

        public UserService(IRepository<User> userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public void Register(RegisterModel model)
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

            var md5 = new MD5CryptoServiceProvider();
            var passwordBytes = Encoding.ASCII.GetBytes(model.Password);
            var hashBytes = md5.ComputeHash(passwordBytes);
            var hash = Encoding.ASCII.GetString(hashBytes);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = hash,
                Username = model.Username
            };

            _userRepository.Add(user);
        }

        public IEnumerable<UserModel> GetAll()
        {
            return _userRepository.GetAll().Select(x => x.ToModel());
        }

        public UserModel Authenticate(LoginModel model)
        {
            var md5 = new MD5CryptoServiceProvider();
            var passwordBytes = Encoding.ASCII.GetBytes(model.Password);
            var hashBytes = md5.ComputeHash(passwordBytes);
            var hash = Encoding.ASCII.GetString(hashBytes);

            var user = _userRepository.GetAll()
                .FirstOrDefault(x => x.Password == hash && x.Username == model.Username);

            if(user == null)
                throw new Exception("Username or password is wrong.");

            //Create token
            var keyBytes = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var jwtTokenHandler = new JwtSecurityTokenHandler();


            var descriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                })
            };

            var token = jwtTokenHandler.CreateToken(descriptor);
            var tokenString = jwtTokenHandler.WriteToken(token);

            var userModel = user.ToModel();
            userModel.Token = tokenString;
            return userModel;
        }

        public bool ValidatePassword(string password)
        {
            var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var match = regex.Match(password);
            return match.Success;
        }
    }
}
