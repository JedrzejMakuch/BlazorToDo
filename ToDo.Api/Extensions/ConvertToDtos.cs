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
                Description= x.Description,
                Status= x.Status,
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
            };

            return toDoItemDto;
        }
    }
}
