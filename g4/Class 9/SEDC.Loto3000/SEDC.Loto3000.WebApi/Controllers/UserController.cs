using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.Models;
using SEDC.Loto3000.WebApi.ViewModels;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody]User user)
        {
            //TODO: add model validation(with action filter) cause in .net core 2.0 we do not have it by default
            //TODO: Use viewModel for the request model
            //TODO: Add ErrorHandlingMiddleware
            _userService.Register(user);
            return Ok(user);
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody]AuthenticateUserViewModel user)
        {
            _userService.Get(user.Email, user.Password, out string token);
            Response.Headers.Add("Authorization", token);
            return Ok(user);
        }
    }
}