using Microsoft.AspNetCore.Mvc;
using CopilotAPI.Interfaces;
using CopilotAPI.Contracts;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _todoServices.CreateTodoAsync(request);
                return Ok(new { message = "Blog post successfully created" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var todo = await _todoServices.GetByIdAsync(id);
            return Ok(new { message = $"Successfully retrieved Todo item with Id {id}.", data = todo });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todo Item  with id {id} not found" });
                }

                await _todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = $" Todo Item  with id {id} successfully updated" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating blog post with id {id}", error = ex.Message });


            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                await _todoServices.DeleteTodoAsync(id);
                return Ok(new { message = $"Todo  with id {id} successfully deleted" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting Todo Item  with id {id}", error = ex.Message });

            }
        }

    }
}