using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;
using ToDo.Services.Services.Contracts;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class EditToDoItemBase : ComponentBase
    {
        public string name;
        public string description;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        IToDoItemService ToDoItemService { get; set; }

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }
        public ToDoItemDto ToDoItemDto { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            httpClient = new HttpClient();
            try
            {
                ToDoItemDto = await ToDoItemService.GetItem(Id);
                description= ToDoItemDto.Description;
                name= ToDoItemDto.Name;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task SaveToDo()
        {
            var editToDo = new ToDoItemPayload()
            {
                Description = description,
                Name = name,
            };

            var validationContext = new ValidationContext(editToDo);
            var validationMessages = new List<ValidationResult>();

            if (Validator.TryValidateObject(editToDo, validationContext, validationMessages))
            {
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7265/api/ToDoItem/{Id}", editToDo);

                if (response.IsSuccessStatusCode)
                {
                    name = string.Empty;
                    description = string.Empty;
                    navigationManager.NavigateTo($"/");
                }
                else
                {
                    System.Console.WriteLine(response.StatusCode);
                }
            }
            else
            {
                System.Console.WriteLine("Validation Failed");
            }
        }

        
    }
}
