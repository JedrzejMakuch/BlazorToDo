using ToDo.Models.Dtos;
using ToDo.Models.Payloads;

namespace ToDo.Services.Services.Contracts
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItemDto>> GetItems();
        Task<ToDoItemDto> GetItem(int id);
        Task<ToDoItemDto> PostItem(ToDoItemPayload toDoItemPayload);
        Task<ToDoItemDto> UpdateItem(int id, ToDoItemPayload toDoItemPayload);
    }
}
