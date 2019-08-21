using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Data;
using DataModels;
using Models;

namespace Business
{
    public class ToDoService : IToDoService
    {
        private readonly IRepository<DtoToDoItem> _toDoItemRepository;
        private readonly IRepository<DtoUser> _userRepository;

        public ToDoService(IRepository<DtoToDoItem> toDoItemRepository, IRepository<DtoUser> userRepository)
        {
            _toDoItemRepository = toDoItemRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<ToDoItemModel> GetAllByUser(int userId)
        {
            return _toDoItemRepository
                .GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => new ToDoItemModel
            {
                Id = x.Id,
                UserId = x.UserId,
                Title = x.Title,
                Description = x.Description,
                Completed = x.Completed
            }).ToList();
        }

        public void AddTodoItem(int userId, ToDoItemModel model)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                throw new Exception("User does not exist.");

            _toDoItemRepository.Add(new DtoToDoItem
            {
                UserId = user.Id,
                Title = model.Title,
                Description = model.Description,
                Completed = false
            });
        }

        public void UpdateTodoItem(int userId, ToDoItemModel model)
        {
            var user = _userRepository.GetById(userId);

            if(user == null)
                throw new Exception("User does not exist.");

            var item = _toDoItemRepository.GetById(model.Id);

            item.Completed = model.Completed;
            item.Description = model.Description;
            item.Title = model.Title;

            _toDoItemRepository.Update(item);
        }

        public void DeleteTodoItem(int itemId)
        {
            var item = _toDoItemRepository
                .GetAll()
                .FirstOrDefault(x => x.Id == itemId);

            _toDoItemRepository.Delete(item);
        }
    }
}
