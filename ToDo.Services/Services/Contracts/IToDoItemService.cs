using ToDo.Models.Dtos;

namespace ToDo.Services.Services.Contracts
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItemDto>> GetItems();
        Task<ToDoItemDto> GetItem(int id);
    }
}
