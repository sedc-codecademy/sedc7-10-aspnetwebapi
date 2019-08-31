using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using SEDC.Loto3000.Models.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.Loto3000.BusinessLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public UserService(IGenericRepository<User> genericRepository, IUserRepository userRepository,
                            IOptions<JwtSettings> options)
        {
            _genericRepository = genericRepository;
            _userRepository = userRepository;
            _jwtSettings = options.Value;
        }
        //TODO: remove the out parameter and create other user model for the result with token, without password
        public User Get(string email, string password, out string token)
        {
            var user = _userRepository.GetUser(email);
            if (user == null)
                throw new ArgumentException($"No user was found for email: {email}", nameof(email));


            var md5 = new MD5CryptoServiceProvider();
            var hashedPasswordRaw = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(hashedPasswordRaw);

            if (user.Password != hashedPassword)
            {
                token = null;
                return null;
            }

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "NotAdmin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            token = tokenHandler.WriteToken(securityToken);
            
            return user;
        }

        public void Register(User user)
        {
            if (UserExists(user.Email))
                throw new ArgumentException($"User with email:{user.Email} exists");

            //TODO: add ConfirmPassword property to User model and validate it here - does it equals to Password

            var md5 = new MD5CryptoServiceProvider();
            byte[] passwordHashedRaw = md5.ComputeHash(Encoding.ASCII.GetBytes(user.Password));
            var passwordHashed = Encoding.ASCII.GetString(passwordHashedRaw);
            user.Password = passwordHashed;

            _genericRepository.Add(user);
        }

        private bool UserExists(string email)
        {
            var user = _userRepository.GetUser(email);
            return user != null;
        }
    }
}
