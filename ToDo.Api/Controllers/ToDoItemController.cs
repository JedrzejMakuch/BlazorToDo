using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Extensions;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;
using ToDo.Repositories.Repositories.Contracts;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemRepository toDoItemRepository;

        public ToDoItemController(IToDoItemRepository toDoItemRepository)
        {
            this.toDoItemRepository = toDoItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetItems()
        {
            try
            {
                var items = await this.toDoItemRepository.GetItems();
                if(items == null)
                {
                    return NotFound();
                }
                else
                {
                    var itemsDto = items.ConvertToDto();
                    return Ok(itemsDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ToDoItemDto>> GetItem(int id)
        {
            try
            {
                var item = await this.toDoItemRepository.GetItem(id);
                if(item == null)
                {
                    return NotFound();
                }
                else
                {
                    var itemDto = item.ConvertToDto();
                    return Ok(itemDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> PostItem([FromBody] ToDoItemPayload toDoItemPayload)
        {
            try
            {
                var newToDoItem = await this.toDoItemRepository.PostItem(toDoItemPayload);
                if(newToDoItem == null)
                {
                    throw new Exception($"Something went wrong");
                }

                var newToDoItemDto = newToDoItem.ConvertToDto();
                return CreatedAtAction(nameof(GetItem), new { id = newToDoItem.Id }, newToDoItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest();
                }

                var item = await this.toDoItemRepository.GetItem(id);
                if(item == null)
                {
                    
                }

                await this.toDoItemRepository.DeleteItem(item);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ToDoItemDto>> UpdateItem([FromBody] ToDoItemPayload toDoItemPayload, int id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var item = await this.toDoItemRepository.GetItem(id);
            if(item == null)
            {
                return NotFound($"ToDoItem with Id: {id} does not exist.");
            }

            var updatedItem = await this.toDoItemRepository.UpdateItem(item, toDoItemPayload);
            var updatedItemDto = updatedItem.ConvertToDto();
            return Ok(updatedItemDto);
        }

    }
}
