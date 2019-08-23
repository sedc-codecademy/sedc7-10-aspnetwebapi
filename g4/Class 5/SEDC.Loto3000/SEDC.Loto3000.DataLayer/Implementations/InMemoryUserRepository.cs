using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System.Linq;

namespace SEDC.Loto3000.DataLayer.Implementations
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _genericRepository;

        public InMemoryUserRepository(IGenericRepository<User> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public User GetUser(string email)
        {
            return _genericRepository.GetAll()
                                .SingleOrDefault(u => u.Email == email);
        }
    }
}
