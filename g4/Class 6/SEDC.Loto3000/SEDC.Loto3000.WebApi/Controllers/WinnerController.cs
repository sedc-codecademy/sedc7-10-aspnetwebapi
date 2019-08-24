using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SEDC.Loto3000.BusinessLayer.Contracts;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Winner")]
    public class WinnerController : Controller
    {
        private readonly IWinnerService _winnerService;

        public WinnerController(IWinnerService winnerService)
        {
            _winnerService = winnerService;
        }

        [HttpPost]
        public IActionResult SetWinners([FromQuery][Required]string drawId)
        {
            _winnerService.SetWinners(drawId);
            return Ok();
        }

        [HttpGet]
        public ActionResult GetWinners([FromQuery][Required]string drawId)
        {
            var result = _winnerService.GetWinners(drawId);
            //TODO: map the result into IEnumerable of viewModel so we can return users full name for each ticket
            return Ok(result);
        }
    }
}