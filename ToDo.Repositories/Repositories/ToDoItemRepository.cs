using Microsoft.EntityFrameworkCore;
using ToDo.Data.Data;
using ToDo.Models.Entities;
using ToDo.Models.Payloads;
using ToDo.Repositories.Repositories.Contracts;

namespace ToDo.Repositories.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly BlazorToDoDbContext blazorToDoContext;

        public ToDoItemRepository(BlazorToDoDbContext blazorToDoContext)
        {
            this.blazorToDoContext = blazorToDoContext;
        }

        public async Task DeleteItem(ToDoItem toDoItem)
        {
            this.blazorToDoContext.ToDoItems.Remove(toDoItem);
            await this.blazorToDoContext.SaveChangesAsync();
        }

        public async Task<ToDoItem> GetItem(int id)
        {
            var item = await this.blazorToDoContext.ToDoItems.FindAsync(id);
            return item;
        }

        public async Task<IEnumerable<ToDoItem>> GetItems()
        {
            var items = await this.blazorToDoContext.ToDoItems.ToListAsync();
            return items;
        }

        public async Task<ToDoItem> PostItem(ToDoItemPayload toDoItemPayload)
        {
            var item = new ToDoItem
            {
                Name = toDoItemPayload.Name,
                Description = toDoItemPayload.Description,
            };

            if (item == null)
            {
                return null;
            }
            else
            {
                var result = await this.blazorToDoContext.ToDoItems.AddAsync(item);
                await this.blazorToDoContext.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<ToDoItem> UpdateItem(ToDoItem toDoItem, ToDoItemPayload toDoItemPayload)
        {
            var item = await this.blazorToDoContext.ToDoItems.FindAsync(toDoItem.Id);
            item.Name = toDoItemPayload.Name;
            item.Description = toDoItemPayload.Description;
            await this.blazorToDoContext.SaveChangesAsync();
            return item;
        }
    }
}
