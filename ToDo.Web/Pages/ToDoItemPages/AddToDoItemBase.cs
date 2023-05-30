using Microsoft.AspNetCore.Components;
using ToDo.Models.Payloads;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class AddToDoItemBase : ComponentBase
    {
        public string name;
        public string description;
        [Inject]
        private HttpClient httpClient { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected override void OnInitialized()
        {
            httpClient = new HttpClient();
        }

        public async Task AddNewToDO()
        {
            var newTODO = new ToDoItemPayload
            {
                Name = name,
                Description = description,
            };

            var validationContext = new ValidationContext(newTODO);
            var validationMessages = new List<ValidationResult>();

            if(Validator.TryValidateObject(newTODO, validationContext, validationMessages))
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7265/api/ToDoItem", newTODO);

                if (response.IsSuccessStatusCode)
                {
                    name = string.Empty;
                    description = string.Empty;
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            else
            {
                Console.WriteLine("Validation Failed");
            }
        }
    }
}
