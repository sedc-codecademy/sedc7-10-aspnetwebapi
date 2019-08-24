using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Phonebook.Controllers
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

        [HttpPost("Authenticate")]
        public ActionResult Authenticate(LoginModel login)
        {

            return Ok();
        }

        [HttpPost("RegisterUser")]
        public ActionResult RegisterUser(RegisterModel register)
        {

            return Ok();
        }
    }
}