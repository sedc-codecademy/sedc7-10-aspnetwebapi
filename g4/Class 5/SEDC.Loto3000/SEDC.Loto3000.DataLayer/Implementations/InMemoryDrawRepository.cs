using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System.Linq;

namespace SEDC.Loto3000.DataLayer.Implementations
{
    public class InMemoryDrawRepository : IDrawRepository
    {
        private readonly IGenericRepository<Draw> _genericRepository;

        public InMemoryDrawRepository(IGenericRepository<Draw> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public Draw GetActiveDraw()
        {
            return _genericRepository.GetAll()
                                .SingleOrDefault(d => d.IsActive);
        }
    }
}
