using Microsoft.AspNetCore.Components;
using System.Net.Http;
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

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            httpClient = new HttpClient();
            try
            {
                ToDoItemDto = await ToDoItemService.GetItem(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public async Task DeleteToDo()
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7265/api/ToDoItem/{Id}");

            if (response.IsSuccessStatusCode)
            {
                navigationManager.NavigateTo($"/");
            }
            else
            {
                System.Console.WriteLine(response.StatusCode);
            }
        }

    }
}
