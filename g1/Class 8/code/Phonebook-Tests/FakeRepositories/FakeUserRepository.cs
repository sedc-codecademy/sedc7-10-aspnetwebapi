using System;
using System.Collections.Generic;
using System.Text;
using DataModels;
using Repository;
using System.Linq;

namespace Phonebook_Tests.FakeRepositories
{
    public class FakeUserRepository : IRepository<DtoUser>
    {
        private List<DtoUser> _users;

        public FakeUserRepository()
        {
            _users = new List<DtoUser>
            {
                new DtoUser
                {
                    Id = 1,
                    FirstName = "Igor",
                    LastName = "Mitkovski",
                    Username = "igor.mitkovski",
                    Password = "123456"
                }
            };
        }

        public IEnumerable<DtoUser> GetAll()
        {
            return _users;
        }

        public void Add(DtoUser entity)
        {
            int id = _users.Max(u => u.Id) + 1;
            entity.Id = id;

            _users.Add(entity);
        }

        public void Update(DtoUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DtoUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
