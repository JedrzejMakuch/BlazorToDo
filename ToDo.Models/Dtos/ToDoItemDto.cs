﻿using ToDo.Api.Shared.Enums;

namespace ToDo.Models.Dtos
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ToDoItemStatus Status { get; set; }
        public DateTime? StatusNew { get; set; }
        public DateTime? StatusInProgress { get; set; }
        public DateTime? StatusCompleted { get; set; }
        public List<CheckboxItem> Checkboxes { get; set; } = new List<CheckboxItem>();
    }
    public class CheckboxItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
    }
}
