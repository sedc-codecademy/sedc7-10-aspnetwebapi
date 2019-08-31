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

        public UserService(IGenericRepository<User> genericRepository, 
                            IUserRepository userRepository,
                            IOptions<JwtSettings> jwtSettings)
        {
            _genericRepository = genericRepository;
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }
        public string AuthenticateAndGetJwtToken(string email, string password)
        {
            var user = _userRepository.GetUser(email);

            if (user?.Password != GetHash(password))
                return null;

            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "NotAdmin"),
                    new Claim(ClaimTypes.Name, user.FullName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
        
        public void Register(User user)
        {
            if (UserExists(user.Email))
                throw new ArgumentException($"User with email:{user.Email} exists");
            
            user.Password = GetHash(user.Password);
            _genericRepository.Add(user);
        }

        private string GetHash(string password)
        {
            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            var rawHash = md5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(rawHash);
        }

        private bool UserExists(string email)
        {
            var user = _userRepository.GetUser(email);
            return user != null;
        }
    }
}
