using Microsoft.EntityFrameworkCore;
using ToDo.Api.Shared.Enums;
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

        public async Task<ToDoItem> ChangeStatus(int id, string direction)
        {
            var toDo = await this.blazorToDoContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
            if(direction == "up")
            {
                switch(toDo.Status)
                {
                    case ToDoItemStatus.New:
                        toDo.Status = ToDoItemStatus.InProgress;
                        toDo.StatusInProgress = DateTime.Now;
                        break;
                    case ToDoItemStatus.InProgress:
                        toDo.Status = ToDoItemStatus.Completed;
                        toDo.StatusCompleted = DateTime.Now;
                        break;
                    case ToDoItemStatus.Completed:
                        toDo.Status = ToDoItemStatus.Completed;
                        break;
                }
            } else if(direction == "down")
            {
                switch(toDo.Status)
                {
                    case ToDoItemStatus.New:
                        toDo.Status = ToDoItemStatus.New;
                        toDo.StatusInProgress = null;
                        toDo.StatusCompleted = null;
                        break;
                    case ToDoItemStatus.InProgress:
                        toDo.Status = ToDoItemStatus.New;
                        toDo.StatusNew = DateTime.Now;
                        toDo.StatusInProgress = null;
                        toDo.StatusCompleted = null;
                        break;
                    case ToDoItemStatus.Completed:
                        toDo.Status = ToDoItemStatus.InProgress;
                        toDo.StatusInProgress = DateTime.Now;
                        toDo.StatusCompleted = null;
                        break;
                }
            }

            await this.blazorToDoContext.SaveChangesAsync();
            return toDo;
        }

        public async Task DeleteItem(ToDoItem toDoItem)
        {
            this.blazorToDoContext.ToDoItems.Remove(toDoItem);
            await this.blazorToDoContext.SaveChangesAsync();
        }

        public async Task<ToDoItem> GetItem(int id)
        {
            var item = await this.blazorToDoContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
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
                Status = ToDoItemStatus.New,
                StatusNew = DateTime.Now,
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
            var item = await this.blazorToDoContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == toDoItem.Id);
            item.Name = toDoItemPayload.Name;
            item.Description = toDoItemPayload.Description;
            item.Status = toDoItem.Status;
            await this.blazorToDoContext.SaveChangesAsync();
            return item;
        }
    }
}
