using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.Loto3000.DataLayer.Implementations
{
    public class InMemoryTicketRepository : ITicketRepository
    {
        private readonly IGenericRepository<Ticket> _genericRepository;

        public InMemoryTicketRepository(IGenericRepository<Ticket> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public IEnumerable<Ticket> GetByDrawId(string drawId)
        {
            return _genericRepository.GetAll()
                                        .Where(t => t.DrawId == drawId);
        }
    }
}
