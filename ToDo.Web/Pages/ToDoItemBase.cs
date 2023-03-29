using Microsoft.AspNetCore.Components;
using ToDo.Models.Dtos;
using ToDo.Services.Services.Contracts;

namespace ToDo.Web.Pages
{
    public class ToDoItemBase : ComponentBase
    {
        [Inject]
        IToDoItemService ToDoItemService { get; set; }
        public IEnumerable<ToDoItemDto> ToDoItemDtos { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ToDoItemDtos = await ToDoItemService.GetItems();
        }
    }
}
