using System.Collections.Generic;
using Data;
using DataModels;

namespace ToDoApp_Tests.FakeRepositories
{
    public class FakeToDoItemRepository : IRepository<DtoToDoItem>
    {
        private readonly List<DtoToDoItem> _toDoItems;

        public FakeToDoItemRepository()
        {
            _toDoItems = new List<DtoToDoItem>()
            {
                new DtoToDoItem() {Id = 1, Completed = false, Description = "Test", Title = "Test", UserId = 1},
                new DtoToDoItem() {Id = 2, Completed = false, Description = "Test 2", Title = "Test 2", UserId = 2}
            };
        }
        public IEnumerable<DtoToDoItem> GetAll()
        {
            return _toDoItems;
        }

        public void Add(DtoToDoItem entity)
        {
            _toDoItems.Add(entity);
        }

        public void Delete(DtoToDoItem entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DtoToDoItem entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
