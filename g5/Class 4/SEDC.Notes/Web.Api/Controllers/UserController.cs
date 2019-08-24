using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if(user == null)
            {
                return NotFound("Username or Password is incorrect!");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            _userService.Register(model);
            return Ok();
        }

    }
}