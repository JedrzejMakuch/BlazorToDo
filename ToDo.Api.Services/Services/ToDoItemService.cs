using ToDo.Api.Services.Services.Contracts;
using ToDo.Models.Entities;
using ToDo.Models.Payloads;
using ToDo.Repositories.Repositories.Contracts;

namespace ToDo.Api.Services.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository toDoItemRepository;

        public ToDoItemService(IToDoItemRepository toDoItemRepository)
        {
            this.toDoItemRepository = toDoItemRepository;
        }

        public async Task<ToDoItem> ChangeStatus(int id, string direction)
        {
            var toDo = await this.toDoItemRepository.ChangeStatus(id, direction);
            return toDo;
        }

        public async Task DeleteItem(ToDoItem toDoItem)
        {
            await this.toDoItemRepository.DeleteItem(toDoItem);
        }

        public async Task<ToDoItem> GetItem(int id)
        {
            var item = await this.toDoItemRepository.GetItem(id);
            return item;
        }

        public async Task<IEnumerable<ToDoItem>> GetItems()
        {
            var items = await this.toDoItemRepository.GetItems();
            return items;
        }

        public async Task<ToDoItem> PostItem(ToDoItemPayload toDoItemPayload)
        {
            var newItem = await this.toDoItemRepository.PostItem(toDoItemPayload);
            return newItem;
        }

        public async Task<ToDoItem> UpdateItem(ToDoItem toDoItem, ToDoItemPayload toDoItemPayload)
        {
            var updatedItem = await this.toDoItemRepository.UpdateItem(toDoItem, toDoItemPayload);
            return updatedItem;
        }
    }
}
