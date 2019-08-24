using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ToDoApp.Controllers
{
    /// <summary>
    /// This is Users controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/values
        [Route("register")]
        [HttpPost]
        public IActionResult Post([FromBody] RegisterModel model)
        {
            _userService.Register(model);
            return Ok();
        }

        [Route("authenticate")]
        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            return Ok(_userService.Authenticate(model));
        }
    }
}
