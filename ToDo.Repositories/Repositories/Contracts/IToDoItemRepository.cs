using ToDo.Models.Dtos;
using ToDo.Models.Entities;
using ToDo.Models.Payloads;

namespace ToDo.Repositories.Repositories.Contracts
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetItems();
        Task<ToDoItem> GetItem(int id);
        Task<ToDoItem> PostItem(ToDoItemPayload toDoItemPayload);
        Task DeleteItem(ToDoItem toDoItem);
        Task<ToDoItem> UpdateItem(ToDoItem toDoItem, ToDoItemPayload toDoItemPayload);
        Task<ToDoItem> ChangeStatus(int id, string direction);
    }
}
