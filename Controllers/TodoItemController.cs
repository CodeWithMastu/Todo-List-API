using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListAPI.Models;
using TodoListAPI.Services;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TodoItemController(AppDbContext context)
        {
            _context = context;
        }

        // Create a To-do Item
        [HttpPost("CreateTodo")]
        public async Task<ActionResult<TodoItem>> CreateItemAsync(TodoItemDto tododto)
        {
            var todoItem = new TodoItem
            {
                Title = tododto.Title,
                Description = tododto.Description,
                Deadline = tododto.Deadline
            };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // Get every single to-do item in the db
        [HttpGet("GetTodoList")]
        public async Task<IEnumerable<TodoItem>> GetItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // Get to-do by date
        [HttpGet("GetTodobyDate")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodobyDateAsync(DateTime createdDate)
        {
            var items = await _context.TodoItems
                .Where(t => t.CreatedDate.Date == createdDate.Date)
                .ToListAsync();
            if (items.Count == 0) return NotFound("No to-do Items where found on this day");
            return Ok(items);
        }

        // Get to-do by Id
        [HttpGet("GetTodobyId")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) 
            {
                return NotFound();
            }
            return Ok(item);
        }

        // Edit the to-do item
        [HttpPut("EditTodo")]
        public async Task<ActionResult> UpdateItemAsync(int id, TodoItemDto tododto)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            todoItem.Title = tododto.Title;
            todoItem.Description = tododto.Description;
            todoItem.UpdatedDate = DateTime.Now;
            todoItem.Deadline = tododto.Deadline;
            todoItem.IsCompleted = tododto.IsCompleted;

            await _context.SaveChangesAsync();
            return Ok(todoItem);
        }

        // Delete the to-do item
        [HttpDelete("DeleteTodo")]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return BadRequest();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return Ok("Todo Item was deleted successfully");
        }


    }
}
