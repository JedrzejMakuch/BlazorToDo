using Microsoft.AspNetCore.Components;
using ToDo.Models.Payloads;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using BootstrapBlazor.Components;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class AddToDoItemBase : ComponentBase
    {
        public string name;
        public string description;
        public List<CheckboxItem> checkboxes = new List<CheckboxItem>();
        [Inject]
        private HttpClient httpClient { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected override void OnInitialized()
        {
            httpClient = new HttpClient();
        }

        public void AddCheckbox()
        {
            checkboxes.Add(new CheckboxItem());
        }

        public void RemoveCheckbox(CheckboxItem checkbox)
        {
            checkboxes.Remove(checkbox);
        }

        public async Task AddNewToDO()
        {
            var newTODO = new ToDoItemPayload
            {
                Name = name,
                Description = description,
                Checkboxes = checkboxes
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
                    checkboxes = new List<CheckboxItem>();
                    navigationManager.NavigateTo("/");
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
