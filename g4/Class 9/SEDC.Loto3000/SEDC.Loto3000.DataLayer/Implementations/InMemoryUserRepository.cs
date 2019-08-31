using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.Loto3000.DataLayer.Implementations
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _genericRepository;

        public InMemoryUserRepository(IGenericRepository<User> genericRepository)
        {
            _genericRepository = genericRepository;
            var email = "stojanco.jefremov@gmail.com";
            if (GetUser(email) != null)
                return;

            var password = "123";
            //TODO: Put code for generating hash into helper class method and use it from there, and refactor all the usings of that code in the solution
            var md5 = new MD5CryptoServiceProvider();
            var hashedPasswordRaw = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(hashedPasswordRaw);
            _genericRepository.Add(new User
            {
                Email = email,
                FullName = "Stojancho Jefremov",
                Id = Guid.NewGuid().ToString(),
                IsAdmin = true,
                Password = hashedPassword
            });
        }
        public User GetUser(string email)
        {
            return _genericRepository.GetAll()
                                .SingleOrDefault(u => u.Email == email);
        }
    }
}
