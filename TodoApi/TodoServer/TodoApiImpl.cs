namespace TodoServer
{
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Todo;
    using Todo.Proto;

    public class TodoApiImpl : TodoApi.TodoApiBase
    {
        private readonly TodoContext _context;

        public TodoApiImpl(TodoContext context)
        {
            _context = context;

            if (!_context.TodoItems.Any())
            {
                _context.TodoItems.Add(new TodoItem {Name = "Item1"});
                _context.SaveChanges();
            }
        }

        public override async Task<TodoItem> GetTodoItem(GetTodoItemRequest request, ServerCallContext context)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (item == null)
            {
                return TodoUtil.NullTodoItem;
            }

            return item;
        }

        public override async Task GetAllTodoItems(Empty request, IServerStreamWriter<TodoItem> responseStream,
            ServerCallContext context)
        {
            foreach (var item in _context.TodoItems)
            {
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task<TodoItem> AddTodoItem(AddTodoItemRequest request, ServerCallContext context)
        {
            var item = request.ToItem();
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return await Task.FromResult(item);
        }

        public override async Task<TodoItem> UpdateTodoItem(UpdateTodoItemRequest request, ServerCallContext context)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (item == null)
            {
                return await Task.FromResult(TodoUtil.NullTodoItem);
            }

            item.Name = request.Name;
            item.IsComplete = request.IsComplete;

            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
            return await Task.FromResult(item);
        }

        public override async Task<Empty> DeleteTodoItem(DeleteTodoItemRequest request, ServerCallContext context)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (item == null)
            {
                return await Task.FromResult(new Empty());
            }

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return await Task.FromResult(new Empty());
        }
    }
}