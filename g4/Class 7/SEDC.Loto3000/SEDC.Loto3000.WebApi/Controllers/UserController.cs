using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.Models;
using SEDC.Loto3000.WebApi.ViewModels;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]User user)
        {
            //TODO: add model validation(with action filter) cause in .net core 2.0 we do not have it by default
            //TODO: Use viewModel for the request model
            //TODO: Add ErrorHandlingMiddleware
            _userService.Register(user);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody]AuthenticateUserViewModel request)
        {
            var jwtToken = _userService.AuthenticateAndGetJwtToken(request.Email, request.Password);
            if (string.IsNullOrWhiteSpace(jwtToken))
                return BadRequest("User was not found");

            Response.Headers.Add("Authorize", jwtToken);
            return Ok();
        }
    }
}