using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TodoListWebApiWithControllers.Models
{
    public class TodoContext: DbContext
    {
        //constructor
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) { }

        //property
        public DbSet<TodoItem> todoitems { get; set; } = null!;
        
    }
}
