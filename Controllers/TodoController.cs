using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController(DatabaseContext db) : ControllerBase
    {
        private readonly DatabaseContext _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _db.Todo.ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todo = await _db.Todo.FindAsync(id);

            if (todo == null)
                return NotFound(new { message = $"Todo {id} não encontrado" });

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(TodoModel todo)
        {
            _db.Todo.Add(todo);
            await _db.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] TodoModel updatedTodo)
        {
            var todo = await _db.Todo.FindAsync(id);

            if (todo == null) {
                return NotFound(new { message = $"Todo {id} não encontrado" });
            }

            todo.Update(updatedTodo);
            _db.Todo.Update(todo);
            await _db.SaveChangesAsync();

            return Ok(todo);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _db.Todo.FindAsync(id);

            if (todo == null) {
                return NotFound(new { message = $"Todo {id} não encontrado" });
            }

             _db.Todo.Remove(todo);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
