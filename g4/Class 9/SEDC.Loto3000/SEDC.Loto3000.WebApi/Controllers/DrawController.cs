using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SEDC.Loto3000.BusinessLayer.Contracts;
using System.ComponentModel.DataAnnotations;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class DrawController : Controller
    {
        private readonly IDrawService _drawService;
        private readonly IConfiguration _configuration;

        public DrawController(IDrawService drawService, IConfiguration configuration)
        {
            _drawService = drawService;
            _configuration = configuration;
        }

        [Route("api/draw")]
        [HttpPost]
        public IActionResult CreateDraw([Required][FromQuery]string adminEmail)
        {
            var draw = _drawService.CreateNew(adminEmail);
            var baseUrl = _configuration.GetValue<string>("ApiBaseUrl");
            var drawUrl = $"{baseUrl}/api/draw/{draw.Id}";//TODO: implement the action
            return Created(drawUrl, draw);
        }

        [HttpPost]
        [Route("api/draw/submit")]
        public IActionResult Submit([Required][FromQuery]string adminEmail)
        {
            var draw = _drawService.SubmitDraw(adminEmail);
            return Ok(draw);
        }
    }
}
