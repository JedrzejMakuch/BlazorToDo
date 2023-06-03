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

        private ToDoItemDto ToDoItemToDelete { get; set; }

        [Parameter]
        public IEnumerable<ToDoItemDto> ToDoItemDtos { get; set; }
        public List<ToDoItemDto> FilteredStatus(ToDoItemStatus status) =>
    ToDoItemDtos.Where(item => item.Status == status).ToList();

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

        public async Task OnDeleteDialogClose(bool accepted)
        {
            if(accepted)
            {
                var response = await httpClient.DeleteAsync($"https://localhost:7265/api/ToDoItem/{ToDoItemToDelete.Id}");

                if (response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine(response.StatusCode);
                    ToDoItemToDelete = null;
                    ToDoItemDtos = await ToDoItemService.GetItems();
                }
                else
                {
                    System.Console.WriteLine(response.StatusCode);
                }
            }

            DeleteDialogOpen = false;
        }

        public void OpenDeleteDialog(ToDoItemDto todo)
        {
            ToDoItemToDelete = todo;
            DeleteDialogOpen = true;
        }
    }
}
