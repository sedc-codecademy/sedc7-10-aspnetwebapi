using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Serilog;
using Services;
using Services.Exceptions;

namespace Web.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            try
            {
                var user = _userService.Authenticate(model.Username, model.Password);

                if (user == null)
                {
                    return NotFound("Username or Password is incorrect!");
                }

                return Ok(user);
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}",
                    ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                _userService.Register(model);
                Log.Information("USER registered with username {username}"
                    , model.Username);
                return Ok("Successfully registered user!");
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}",
                    ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }

    }
}