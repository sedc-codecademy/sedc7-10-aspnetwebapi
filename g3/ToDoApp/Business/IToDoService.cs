using System.Collections.Generic;
using Models;

namespace Business
{
    public interface IToDoService
    {
        IEnumerable<ToDoItemModel> GetAllByUser(int userId);
        void AddTodoItem(int userId, ToDoItemModel model);
        void UpdateTodoItem(int userId, ToDoItemModel model);
        void DeleteTodoItem(int itemId);
    }
}
