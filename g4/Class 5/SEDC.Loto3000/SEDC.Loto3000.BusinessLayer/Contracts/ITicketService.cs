using System.Collections.Generic;

namespace SEDC.Loto3000.BusinessLayer.Contracts
{
    public interface ITicketService
    {
        void SubmitTicket(IEnumerable<ushort> pickedNumbers, string userEmail);
    }
}
