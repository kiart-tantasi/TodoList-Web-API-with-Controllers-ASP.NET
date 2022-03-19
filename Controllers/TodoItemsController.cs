#nullable disable
// from adding a scaffolded item in Controllers folder
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListWebApiWithControllers.Models;

namespace TodoListWebApiWithControllers.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        // setting up "_context"
        private readonly TodoContext _context;
        // constructor (put param into _context)
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> Gettodoitems() // <IEnumerable<TodoItem>>
        {
            // return await _context.todoitems.ToListAsync(); // ToListAsync for the whole array of todo items
            return await _context.todoitems.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id) // <TodoItem>
        {
            var todoItem = await _context.todoitems.FindAsync(id); // FindAsync(id) for one item

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO) // <IActionResult>
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.todoitems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.isComplete = todoItemDTO.isComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when  (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();

            // modifying item (Before DTO)
            /*_context.Entry(todoItemDTO).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();*/
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Name = todoItemDTO.Name,
                isComplete = todoItemDTO.isComplete,
            };

            _context.todoitems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.todoitems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.todoitems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.todoitems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem)
        {
            return new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                isComplete = todoItem.isComplete
            };
        }
    }
}
