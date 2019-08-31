using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.WebApi.ViewModels;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [Authorize(Roles = "NotAdmin")]
    [Produces("application/json")]
    [Route("api/Ticket")]
    public class TicketController : BaseController
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
            var addedTicket = _ticketService.SubmitTicket(bodyRequest.PickedNumbers, GetEmailOfLoggedUser());
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