using SEDC.Loto3000.Models;
using System.Collections.Generic;

namespace SEDC.Loto3000.DataLayer.Contracts
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetByDrawId(string drawId);
    }
}
