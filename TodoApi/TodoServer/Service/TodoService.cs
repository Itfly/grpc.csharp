namespace TodoServer.Service
{
    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Todo.Proto;
    using TodoServer.Storage;

    public interface ITodoService
    {
        Task<TodoItem> GetAsync(long id);
        IEnumerable<TodoItem> GetAll();
        Task<TodoItem> AddAsync(TodoItem item);
        Task<TodoItem> UpdateAsync(TodoItem item);
        Task DeleteAsync(long id);
    }

    public class TodoService : ITodoService
    {
        private readonly ILogger<TodoService> _logger;

        private TodoContext _todoContext;

        public TodoService(ILogger<TodoService> logger, TodoContext todoContext)
        {
            _logger = logger;
            _todoContext = todoContext;
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            await _todoContext.TodoItems.AddAsync(item);
            await _todoContext.SaveChangesAsync();
            return await Task.FromResult(item);
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (item == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo Item not found"));
            }

            _todoContext.TodoItems.Remove(item);
            await _todoContext.SaveChangesAsync();
        }

        public IEnumerable<TodoItem> GetAll()
        {
            foreach(var item in _todoContext.TodoItems)
            {
                yield return item;
            }
        }

        public async Task<TodoItem> GetAsync(long id)
        {
            var item = await _todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (item == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo Item not found"));
            }

            return item;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            var old = await _todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == item.Id);
            if (old == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo Item not found"));
            }

            old.Name = item.Name;
            old.IsComplete = item.IsComplete;

            _todoContext.TodoItems.Update(old);
            await _todoContext.SaveChangesAsync();
            return await Task.FromResult(old);
        }
    }
}
