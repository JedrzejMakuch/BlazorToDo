using ToDo.Api.Shared.Enums;

namespace ToDo.Models.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ToDoItemStatus Status { get; set; }
        public DateTime? StatusNew { get; set; }
        public DateTime? StatusInProgress { get; set; }
        public DateTime? StatusCompleted { get; set; }
    }
}
