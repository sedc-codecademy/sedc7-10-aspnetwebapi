using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services;
using Services.Exceptions;

namespace ToDoApp.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Authentication action
        /// </summary>
        /// <param name="model">Login model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                throw new ToDoException("Username or password is incorrect");
            
            _logger.LogInformation($"The user: {user.Username} logged in.");
            return Ok(user);
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model">Register model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            _userService.Register(model);
            return Ok();
        }
    }
}