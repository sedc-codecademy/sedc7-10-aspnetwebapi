using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.Loto3000.DataLayer.Implementations
{
    public class InMemoryWinnerRepository : IWinnerRepository
    {
        private readonly IGenericRepository<Winner> _genericRepository;

        public InMemoryWinnerRepository(IGenericRepository<Winner> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddMany(IEnumerable<Winner> winners)
        {
            foreach (var item in winners)
                _genericRepository.Add(item);
        }

        public IEnumerable<Winner> GetByDrawId(string drawId)
        {
            return _genericRepository.GetAll()
                                    .Where(w => w.DrawId == drawId);
        }
    }
}
