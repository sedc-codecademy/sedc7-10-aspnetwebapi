using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IssuesData;
using IssuesModels;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IssueWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIssueContext _context;

        public UsersController(IIssueContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // POST: api/Users
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] CredentialsViewModel credentialsViewModel)
        {
            var validPassword = AuthenticationHelper.GetPasswordHash("lozinka");
            var sentPassword = AuthenticationHelper.GetPasswordHash(credentialsViewModel.Password);

            if (validPassword != sentPassword)
            {
                return Unauthorized();
            }

            User user = new User
            {
                Username = credentialsViewModel.Username,
                Name = credentialsViewModel.Username,
                Country = "Macedonia"
            };

            var token = AuthenticationHelper.GenerateToken(user, DateTime.UtcNow.AddHours(1));
            return token;
        }

    }
}