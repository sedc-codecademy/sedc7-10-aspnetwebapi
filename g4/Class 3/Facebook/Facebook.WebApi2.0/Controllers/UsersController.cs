using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Facebook.WebApi.Models;
using Facebook.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
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

        [HttpGet]
        public IEnumerable<User> GetUsers([FromQuery]DateTime? startDate,
            [FromQuery]DateTime? endDate, 
            [FromHeader(Name = "Request-ID")][Required]string requestId)
        {
            var users = s_users;

            if (startDate.HasValue)
                users = users.Where(u => u.Birthday >= startDate)
                            .ToList();

            if (endDate.HasValue)
                users = users.Where(u => u.Birthday <= endDate)
                            .ToList();

            Response.Headers.Add("Request-ID", requestId);
            return users;
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult GetByUserName([FromRoute]string username)
        {
            var user = s_users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound("The user does not exist");
            
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateNewUser(User user)
        {
            if (s_users.Any(u => u.Username == user.Username))
                return StatusCode((int)HttpStatusCode.Conflict ,"The username already exists");

            s_users.Add(user);
            string baseUrl = @"http://localhost:62487";

            return Created($"{baseUrl}/api/users/{user.Username}", user);
        }

        [HttpPut("{username}")]
        public IActionResult UpdateUser(UpdateUserViewModel viewModel, string username)
        {
            var user = s_users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound($"User with username: {username} does not exist");

            user.Birthday = viewModel.Birthday;
            user.Email = viewModel.Email;
            user.FirstName = viewModel.FirstName;
            user.Gender = viewModel.Gender;
            user.LastName = viewModel.LastName;
            user.Password = viewModel.Password;

            return Ok(user);
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            var user = s_users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return StatusCode((int)HttpStatusCode.NotFound,
                    $"Username {username} does not exist");

            s_users.Remove(user);
            return NoContent();
        }
    }
}