using Microsoft.AspNetCore.Components;
using ToDo.Models.Dtos;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class DisplayToDoItemsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ToDoItemDto> ToDoItemDtos { get; set; }
    }
}
