using System.Collections.Generic;
using System.Linq;
using Data;
using DataModels;
using Models;
using Services.Exceptions;

namespace Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IRepository<DtoToDoItem> _toDoItemRepository;
        private readonly IRepository<DtoUser> _userRepository;

        public ToDoItemService(IRepository<DtoToDoItem> toDoItemRepository, IRepository<DtoUser> userRepository)
        {
            _toDoItemRepository = toDoItemRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<ToDoModel> GetUserToDoItems(int userId)
        {
            return _toDoItemRepository.GetAll().Where(x => x.UserId == userId).Select(x => new ToDoModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Completed = x.Completed,
                UserId = x.UserId
            }).ToList();
        }

        public ToDoModel GetToDoItem(int id, int userId)
        {
            var item = _toDoItemRepository.GetAll().FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if(item == null)
                throw new ToDoException("ToDo item not found.");

            return new ToDoModel
            {
                Id = item.Id,
                UserId = item.UserId,
                Completed = item.Completed,
                Description = item.Description,
                Title = item.Title
            };
        }

        public void AddToDoItem(ToDoModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
                throw new ToDoException("Title is required field");

            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == model.UserId);

            if(user == null)
                throw new ToDoException("User does not exist");

            var todoItem = new DtoToDoItem
            {
                UserId = user.Id,
                Completed = false,
                Description = model.Description,
                Title = model.Title
            };

            _toDoItemRepository.Add(todoItem);
        }

        public void ChangeCompletenessStatus(int id, int userId)
        {
            var item = _toDoItemRepository.GetAll().FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (item == null)
                throw new ToDoException("ToDo item not found.");

            item.Completed = !item.Completed;

            _toDoItemRepository.Update(item);
        }

        public void DeleteItem(int id, int userId)
        {
            var item = _toDoItemRepository.GetAll().FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (item == null)
                throw new ToDoException("ToDo item not found.");

            _toDoItemRepository.Delete(item);
        }
    }
}
