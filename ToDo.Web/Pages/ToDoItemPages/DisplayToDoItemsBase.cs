using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using ToDo.Api.Shared.Enums;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;
using ToDo.Services.Services.Contracts;
using ToDo.Web.Pages.ToDoItemPages.Modals;

namespace ToDo.Web.Pages.ToDoItemPages
{
    public class DisplayToDoItemsBase : ComponentBase
    {
        [Inject]
        HttpClient httpClient { get; set; }

        [Inject]
        IToDoItemService ToDoItemService { get; set; }

        public ToDoItemDto ToDoItemDto { get; set; }

        [Parameter]
        public IEnumerable<ToDoItemDto> ToDoItemDtos { get; set; }
        public List<ToDoItemDto> FilteredStatus(ToDoItemStatus status) =>
    ToDoItemDtos.Where(item => item.Status == status).ToList();

        public async Task DeleteToDo(int id)
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7265/api/ToDoItem/{id}");

            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine(response.StatusCode);
                ToDoItemDtos = await ToDoItemService.GetItems();
            }
            else
            {
                System.Console.WriteLine(response.StatusCode);
            }
        }

        public async Task ChangeStatus(int id, string direction)
        {
            ToDoItemDto = await ToDoItemService.GetItem(id);
            var statusChangedToDo = new ToDoItemPayload()
            {
                Status = ToDoItemDto.Status,
            };

            var response = await httpClient.PutAsJsonAsync($"api/ToDoItem/{id}/{direction}", statusChangedToDo);

            if(response.IsSuccessStatusCode)
            {
                ToDoItemDtos = await ToDoItemService.GetItems();
            }

        }
        public bool DeleteDialogOpen { get; set; }

        public void OnDeleteDialogClose(bool accepted)
        {
            DeleteDialogOpen = false;
        }

        public void OpenDeleteDialog(int id)
        {
            DeleteToDo(id);
            DeleteDialogOpen = true;
        }
    }
}
