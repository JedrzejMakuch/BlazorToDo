using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;
using ToDo.Services.Services.Contracts;
using CheckboxItem = ToDo.Models.Payloads.CheckboxItem;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class EditToDoItemBase : ComponentBase
    {
        public string name;
        public string description;
        public List<CheckboxItem> checkboxes = new List<CheckboxItem>();

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
                description = ToDoItemDto.Description;
                name = ToDoItemDto.Name;
                checkboxes = ToDoItemDto.Checkboxes.Select(x => new CheckboxItem
                {
                    Description = x.Description,
                    Id = x.Id,
                    IsChecked = x.IsChecked
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void RemoveCheckbox(CheckboxItem checkbox)
        {
            checkboxes.Remove(checkbox);
        }

        public void AddCheckbox()
        {
            checkboxes.Add(new CheckboxItem());
        }

        public async Task SaveToDo()
        {
            var editToDo = new ToDoItemPayload()
            {
                Description = description,
                Name = name,
                Checkboxes = checkboxes
            };

            var response = await httpClient.PutAsJsonAsync($"https://localhost:7265/api/ToDoItem/{Id}", editToDo);

            if (response.IsSuccessStatusCode)
            {
                name = string.Empty;
                description = string.Empty;
                checkboxes = new List<CheckboxItem>();
                navigationManager.NavigateTo($"/");
            }
            else
            {
                System.Console.WriteLine(response.StatusCode);
            }
        }
    }
}
