using ToDo.Api.Shared.Enums;

namespace ToDo.Models.Dtos
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ToDoItemStatus Status { get; set; }
    }
}
