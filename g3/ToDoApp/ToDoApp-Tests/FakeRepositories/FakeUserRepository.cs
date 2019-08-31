using System.Collections.Generic;
using System.Linq;
using Data;
using DataModels;

namespace ToDoApp_Tests.FakeRepositories
{
    public class FakeUserRepository : IRepository<DtoUser>
    {
        private readonly List<DtoUser> _users;

        public FakeUserRepository()
        {
            _users = new List<DtoUser>
            {
                new DtoUser {Id = 1, FirstName = "Test", LastName = "Test", Password = "Test", ToDoList = new List<DtoToDoItem>(), Username = "Test"},
                new DtoUser {Id = 2, FirstName = "Test_2", LastName = "Test", Password = "Test_2", ToDoList = new List<DtoToDoItem>(), Username = "Test_2"}
            };
        }

        public IEnumerable<DtoUser> GetAll()
        {
            return _users;
        }

        public void Add(DtoUser entity)
        {
            entity.Id = _users.Max(x => x.Id) + 1;
            _users.Add(entity);
        }

        public void Delete(DtoUser entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DtoUser entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
