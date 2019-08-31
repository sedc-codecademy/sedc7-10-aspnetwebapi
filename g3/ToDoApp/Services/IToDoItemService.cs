using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IToDoItemService
    {
        IEnumerable<ToDoModel> GetUserToDoItems(int userId);
        ToDoModel GetToDoItem(int id, int userId);
        void AddToDoItem(ToDoModel model);
        void ChangeCompletenessStatus(int id, int userId);
        void DeleteItem(int id, int userId);
    }
}
