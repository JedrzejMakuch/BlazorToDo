using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;
using ToDo.Services.Services.Contracts;
using CheckboxItem = ToDo.Models.Payloads.CheckboxItem;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class EditToDoItemBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        IToDoItemService ToDoItemService { get; set; }

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        public ToDoItemDto ToDoItemDto { get; set; }

        public ToDoItemPayload payload = new ToDoItemPayload();
        public string ErrorMessage { get; set; }

        public List<ValidationResult> validationResults;
        public List<ValidationResult> validationResultsCheckboxes;

        protected override async Task OnInitializedAsync()
        {
            httpClient = new HttpClient();
            try
            {
                ToDoItemDto = await ToDoItemService.GetItem(Id);
                payload.Description = ToDoItemDto.Description;
                payload.Name = ToDoItemDto.Name;
                payload.Checkboxes = ToDoItemDto.Checkboxes.Select(x => new CheckboxItem
                {
                    Description = x.Description,
                    Id = x.Id,
                    IsChecked = x.IsChecked,
                    ValidationResults = new List<ValidationResult>()
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void RemoveCheckbox(CheckboxItem checkbox)
        {
            payload.Checkboxes.Remove(checkbox);
        }

        public void AddCheckbox()
        {
            payload.Checkboxes.Add(new CheckboxItem
            {
                ValidationResults = new List<ValidationResult>()
            });
        }

        public async Task SaveToDo()
        {
            validationResults = Validate(payload);

            foreach (var checkbox in payload.Checkboxes)
            {
                checkbox.ValidationResults = ValidateCheckbox(checkbox);
            }

            var allValidationResults = payload.Checkboxes.SelectMany(c => c.ValidationResults).ToList();

            if (validationResults.Count == 0 && allValidationResults.Count == 0)
            {
                var editToDo = new ToDoItemPayload()
                {
                    Description = payload.Description,
                    Name = payload.Name,
                    Checkboxes = payload.Checkboxes
                };

                var response = await httpClient.PutAsJsonAsync($"https://localhost:7265/api/ToDoItem/{Id}", editToDo);

                if (response.IsSuccessStatusCode)
                {
                    payload.Name = string.Empty;
                    payload.Description = string.Empty;
                    payload.Checkboxes = new List<CheckboxItem>();
                    navigationManager.NavigateTo($"/");
                }
                else
                {
                    System.Console.WriteLine(response.StatusCode);
                    System.Console.WriteLine("response failed");
                }
            }
        }

        public List<ValidationResult> Validate(ToDoItemPayload newToDo)
        {
            var validationMessage = new List<ValidationResult>();
            var validationContext = new ValidationContext(newToDo);

            Validator.TryValidateObject(newToDo, validationContext, validationMessage);

            return validationMessage;
        }

        public List<ValidationResult> ValidateCheckbox(CheckboxItem checkbox)
        {
            var validationMessage = new List<ValidationResult>();

            var validationContext = new ValidationContext(checkbox);
            Validator.TryValidateObject(checkbox, validationContext, validationMessage);

            return validationMessage;
        }
    }
}
