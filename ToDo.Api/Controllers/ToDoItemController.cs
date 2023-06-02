using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Extensions;
using ToDo.Api.Services.Services.Contracts;
using ToDo.Models.Dtos;
using ToDo.Models.Payloads;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService toDoItemService;

        public ToDoItemController(IToDoItemService toDoItemService)
        {
            this.toDoItemService = toDoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetItems()
        {
            try
            {
                var items = await this.toDoItemService.GetItems();
                if (items == null)
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
                var item = await this.toDoItemService.GetItem(id);
                if (item == null)
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
                var newToDoItem = await this.toDoItemService.PostItem(toDoItemPayload);
                if (newToDoItem == null)
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
                if (id == null)
                {
                    return BadRequest();
                }

                var item = await this.toDoItemService.GetItem(id);
                if (item == null)
                {

                }

                await this.toDoItemService.DeleteItem(item);
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
            if (id == null)
            {
                return BadRequest();
            }

            var item = await this.toDoItemService.GetItem(id);
            if (item == null)
            {
                return NotFound($"ToDoItem with Id: {id} does not exist.");
            }

            var updatedItem = await this.toDoItemService.UpdateItem(item, toDoItemPayload);
            var updatedItemDto = updatedItem.ConvertToDto();
            return Ok(updatedItemDto);
        }

        [HttpPut("{id:int}/{direction}")]
        public async Task<ActionResult<ToDoItemDto>> ChangeStatus(int id, string direction)
        {
            var toDoItem = await this.toDoItemService.GetItem(id);
            if(toDoItem == null)
            {
                return NotFound();
            }

            var toDo = await this.toDoItemService.ChangeStatus(id, direction);
            var toDoDto = toDo.ConvertToDto();
            return Ok(toDoDto);

            //var toDo = direction == "up" ? await this.toDoItemService.StatusUp(id, direction) : await this.toDoItemService.StatusDown(id, direction);
            //if (direction == "up")
            //{
            //    await this.toDoItemService.StatusUp(id, direction);
            //} else if(direction == "down")
            //{
            //    await this.toDoItemService.StatusDown(id, direction);
            //}
            //var convertedToDo = toDo.ConvertToDto();
            //return Ok(convertedToDo);
        }
    }
}
