using SEDC.Loto3000.Models;
using System.Collections.Generic;

namespace SEDC.Loto3000.BusinessLayer.Contracts
{
    public interface ITicketService
    {
        Ticket SubmitTicket(IEnumerable<ushort> pickedNumbers, string userEmail);
    }
}
