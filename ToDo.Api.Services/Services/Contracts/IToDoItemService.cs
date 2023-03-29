using ToDo.Models.Entities;
using ToDo.Models.Payloads;

namespace ToDo.Api.Services.Services.Contracts
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetItems();
        Task<ToDoItem> GetItem(int id);
        Task<ToDoItem> PostItem(ToDoItemPayload toDoItemPayload);
        Task DeleteItem(ToDoItem toDoItem);
        Task<ToDoItem> UpdateItem(ToDoItem toDoItem, ToDoItemPayload toDoItemPayload);
    }
}
