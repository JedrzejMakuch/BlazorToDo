using ToDo.Models.Dtos;
using ToDo.Models.Entities;

namespace ToDo.Api.Extensions
{
    public static class ConvertToDtos
    {
        public static IEnumerable<ToDoItemDto> ConvertToDto(this IEnumerable<ToDoItem> toDoItems)
        {
            var toDoItemDtos = toDoItems.Select(x => new ToDoItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Status = x.Status,
                StatusCompleted = x.StatusCompleted,
                StatusInProgress = x.StatusInProgress,
                StatusNew = x.StatusNew,
                Checkboxes = x.Checkboxes.Select(y => new Models.Dtos.CheckboxItem
                {
                    Id = y.Id,
                    IsChecked = y.IsChecked,
                    Description = y.Description
                }).ToList()
            }).ToList();

            return toDoItemDtos;
        }

        public static ToDoItemDto ConvertToDto(this ToDoItem toDoItem) 
        {
            var toDoItemDto = new ToDoItemDto
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                Description = toDoItem.Description,
                Status = toDoItem.Status,
                StatusCompleted = toDoItem.StatusCompleted,
                StatusInProgress = toDoItem.StatusInProgress,
                StatusNew = toDoItem.StatusNew,
                Checkboxes = toDoItem.Checkboxes.Select(x => new Models.Dtos.CheckboxItem
                {
                    Id = x.Id,
                    IsChecked = x.IsChecked,
                    Description = x.Description
                }).ToList()
            };

            return toDoItemDto;
        }
    }
}
