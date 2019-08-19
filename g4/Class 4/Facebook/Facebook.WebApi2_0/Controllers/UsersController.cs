using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Facebook.WebApi2_0.Filters;
using Facebook.WebApi2_0.Models;
using Facebook.WebApi2_0.Services.Contracts;
using Facebook.WebApi2_0.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.WebApi2_0.Controllers
{
    [ServiceFilter(typeof(ModelValidationFilter))]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        public IEnumerable<User> GetUsers([FromQuery]DateTime? startDate,
            [FromQuery]DateTime? endDate, 
            [FromHeader(Name = "Request-ID")][Required]string requestId)
        {
            Response.Headers.Add("Request-ID", requestId);

            return _usersService.GetUsers(startDate, endDate);
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult GetByUserName([FromRoute]string username)
        {
            var user = _usersService.GetByUsername(username);
            
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateNewUser([FromBody]User user)
        {
            var serviceUser = _usersService.AddUser(user);

            string baseUrl = @"http://localhost:62487";
            return Created($"{baseUrl}/api/users/{serviceUser.Username}", serviceUser);
        }

        [HttpPut("{username}")]
        public IActionResult UpdateUser([FromBody]UpdateUserViewModel viewModel, string username)
        {
            var user = Models.User.FromViewModel(viewModel);
            user.Username = username;
            var serviceUser = _usersService.UpdateUser(user);
            return Ok(serviceUser);
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            _usersService.DeleteUser(username);

            return NoContent();
        }
    }
}