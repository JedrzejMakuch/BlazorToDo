using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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
            var toDo = await blazorToDoContext.ToDoItems.Include(x => x.Checkboxes).FirstOrDefaultAsync(x => x.Id == id);
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

            blazorToDoContext.ToDoItems.Update(toDo);
            await blazorToDoContext.SaveChangesAsync();
            return toDo;
        }

        public async Task DeleteItem(ToDoItem toDoItem)
        {
            blazorToDoContext.RemoveRange(toDoItem.Checkboxes);
            blazorToDoContext.Remove(toDoItem);
            await blazorToDoContext.SaveChangesAsync();
        }

        public async Task<ToDoItem> GetItem(int id)
        {
            var item = await blazorToDoContext.ToDoItems.Include(x => x.Checkboxes).FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<IEnumerable<ToDoItem>> GetItems()
        {
            var items = await blazorToDoContext.ToDoItems.Include(x => x.Checkboxes).ToListAsync();
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
                Checkboxes = toDoItemPayload.Checkboxes.Select(x => new Models.Entities.CheckboxItem
                {
                    IsChecked = false,
                    Description = x.Description
                }).ToList()
            };

            if (item == null)
            {
                return null;
            }
            else
            {
                var result = await blazorToDoContext.ToDoItems.AddAsync(item);
                await blazorToDoContext.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<ToDoItem> UpdateItem(ToDoItem toDoItem, ToDoItemPayload toDoItemPayload)
        {
            var item = await blazorToDoContext.ToDoItems.Include(x => x.Checkboxes).FirstAsync(x => x.Id == toDoItem.Id);
            var checkboxes = blazorToDoContext.Checkboxes.Where(x => x.ToDoItemId == toDoItem.Id).ToList();

            foreach (var checkbox in checkboxes)
            {
                var checkboxToUpdate = toDoItemPayload.Checkboxes.FirstOrDefault(x => x.Id == checkbox.Id);
                if (checkboxToUpdate != null)
                {
                    checkbox.IsChecked = checkboxToUpdate.IsChecked;
                    checkbox.Description = checkboxToUpdate.Description;
                }
                else
                {
                    blazorToDoContext.Checkboxes.Remove(checkbox);
                }
            }

            foreach (var checkbox in toDoItemPayload.Checkboxes)
            {
                if(checkbox.Id == 0)
                {
                    var newCheckbox = new Models.Entities.CheckboxItem
                    {
                        IsChecked = checkbox.IsChecked,
                        Description = checkbox.Description,
                    };
                    item.Checkboxes.Add(newCheckbox);
                }
            }


            item.Name = toDoItemPayload.Name;
            item.Description = toDoItemPayload.Description;
            item.Status = toDoItem.Status;

            blazorToDoContext.ToDoItems.Update(item);
            await blazorToDoContext.SaveChangesAsync();
            return item;
        }
    }
}
