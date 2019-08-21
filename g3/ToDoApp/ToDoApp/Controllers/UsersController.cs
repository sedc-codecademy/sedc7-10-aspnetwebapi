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
        public void Post([FromBody] UserModel model)
        {
            _userService.Register(model);
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<UserModel> GetAll()
        {
            return _userService.GetAll();
        }
    }
}
