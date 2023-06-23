using System.Net.Http.Json;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;
using ToDo.Services.Services.Contracts;

namespace ToDo.Services.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly HttpClient httpClient;

        public ToDoItemService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ToDoItemDto> GetItem(int id)
        {
            try
            {
                var response = await this.httpClient.GetAsync($"api/ToDoItem/{id}");
                if(response.IsSuccessStatusCode) 
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent) 
                    {
                        return default(ToDoItemDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ToDoItemDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }

        public async Task<IEnumerable<ToDoItemDto>> GetItems()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/ToDoItem");
                if(response.IsSuccessStatusCode) 
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(IEnumerable<ToDoItemDto>);
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ToDoItemDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }

        public async Task<ToDoItemDto> PostItem(ToDoItemPayload toDoItemPayload)
        {
            try
            {
                var response = await this.httpClient.PostAsJsonAsync("api/ToDoItem", toDoItemPayload);
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ToDoItemDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ToDoItemDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ToDoItemDto> UpdateItem(int id, ToDoItemPayload toDoItemPayload)
        {
            try
            {
                var response = await this.httpClient.PutAsJsonAsync($"api/ToDoItem{id}", toDoItemPayload);
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ToDoItemDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ToDoItemDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
