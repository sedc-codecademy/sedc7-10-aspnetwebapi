using IssuesModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IssueWebApi.Controllers
{
    public class AuthenticationHelper
    {
        public static string GenerateToken(User user, DateTime expiry)
        {
            // We create an instance of JwtSecurityTokenHandler that lets us create and validate JWT tokens
            var tokenHandler = new JwtSecurityTokenHandler();
            // Since all methods for security and algorithms work with byte[] we convert the secret into one ( the secret is added as a configuration string )
            var key = Encoding.ASCII.GetBytes("Secret value that should be read from config");

            // All data we need to create the token is set here
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Claims ( name and country )
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{user.Name}"),
                    new Claim("Country", $"{user.Country}"),
                }),
                // Time when the token will expire and it will need to be refreshed
                Expires = expiry,
                // Incorporating the secret in the signiture hashed with the SHA256 algorithm
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            // We create a new token with the description that we created
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetPasswordHash(string password)
        {
            // We create an instance of the SHA512Managed that will help us create the hash
            var sha = new SHA512Managed();
            // We create the hash from the password
            var shaData = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            // We get the hash string
            var hashedPassword = Encoding.Unicode.GetString(shaData);
            return hashedPassword;
        }

    }
}
