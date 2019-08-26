using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.WebApi.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Ticket")]
    [Authorize(Roles = "NotAdmin")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IConfiguration _configuration;

        public TicketController(ITicketService ticketService, IConfiguration configuration)
        {
            _ticketService = ticketService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult SubmitTicket([FromBody]SubmitTicketViewModel bodyRequest)
        {
            var email = User.Claims
                            .SingleOrDefault(c => c.Type == ClaimTypes.Email)
                            ?.Value;
            var addedTicket = _ticketService.SubmitTicket(bodyRequest.PickedNumbers, email);
            var apiBaseUrl = _configuration.GetValue<string>("ApiBaseUrl");
            var response = new
            {
                WinnersBoardLink = $"{apiBaseUrl}/api/winner?drawId={addedTicket.DrawId}",
                Message = "You should wait for the draw and if you get a prize you will see your name on the board"
            };
            var ticketLink = $"{apiBaseUrl}/api/ticket/{addedTicket.Id}";//TODO: implement this(HttpGet action to read specific ticket)
            return Created(ticketLink, response);
        }
    }
}