using System.ComponentModel.DataAnnotations;
using ToDo.Api.Shared.Enums;

namespace ToDo.Models.Payloads
{
    public class ToDoItemPayload
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public ToDoItemStatus? Status { get; set; }
        public List<CheckboxItem> Checkboxes { get; set; }
    }
    public class CheckboxItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
    }
}
