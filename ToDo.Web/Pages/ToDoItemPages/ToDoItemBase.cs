using Microsoft.AspNetCore.Components;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;
using ToDo.Services.Services.Contracts;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class ToDoItemBase : ComponentBase
    {
        [Inject]
        IToDoItemService ToDoItemService { get; set; }
        public IEnumerable<ToDoItemDto> ToDoItemDtos { get; set; }
        public ToDoItemPayload ToDoItemPayload { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ToDoItemDtos = await ToDoItemService.GetItems();
        }
    }
}
