using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using ToDo.Models.Payloads;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class AddToDoItemBase : ComponentBase
    {
        public List<CheckboxItem> checkboxes = new List<CheckboxItem>();
        [Inject]
        private HttpClient httpClient { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        public List<ValidationResult> validationResults;

        public ToDoItemPayload payload = new ToDoItemPayload();

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
            validationResults = Validate(payload);

            if (payload.Checkboxes == null)
            {
                payload.Checkboxes = new List<CheckboxItem>();
            }

            if (validationResults.Count == 0)
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7265/api/ToDoItem", payload);

                if (response.IsSuccessStatusCode)
                {
                    payload.Name = string.Empty;
                    payload.Description = string.Empty;
                    checkboxes = new List<CheckboxItem>();
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            else
            {
                StateHasChanged();
            }
        }

        public List<ValidationResult> Validate(ToDoItemPayload newToDo)
        {
            var validationMessage = new List<ValidationResult>();
            var validationContext = new ValidationContext(newToDo);

            Validator.TryValidateObject(newToDo, validationContext, validationMessage);

            return validationMessage;
        }
    }
}
