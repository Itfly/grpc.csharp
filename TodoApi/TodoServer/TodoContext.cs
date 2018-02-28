namespace TodoServer
{
    using Microsoft.EntityFrameworkCore;
    using Todo.Proto;

    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) 
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}