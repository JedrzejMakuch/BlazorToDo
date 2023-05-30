using Microsoft.AspNetCore.Components;
using ToDo.Models.Dtos;
using ToDo.Services.Services.Contracts;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class ToDoItemDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        IToDoItemService ToDoItemService { get; set; }
        public ToDoItemDto ToDoItemDto { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ToDoItemDto = await ToDoItemService.GetItem(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
