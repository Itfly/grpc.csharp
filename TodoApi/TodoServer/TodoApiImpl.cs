namespace TodoServer
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Todo;
    using Todo.Proto;

    public class TodoApiImpl : TodoApi.TodoApiBase
    {
        private readonly IServiceProvider _provider;

        public TodoApiImpl(IServiceProvider provider)
        {
            _provider = provider;
        }

        public override async Task<TodoItem> GetTodoItem(GetTodoItemRequest request, ServerCallContext context)
        {
            var todoContext = GetScopedTodoContext();
            var item = await todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (item == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo Item not found"));
            }

            return item;
        }

        public override async Task GetAllTodoItems(Empty request, IServerStreamWriter<TodoItem> responseStream,
            ServerCallContext context)
        {
            var todoContext = GetScopedTodoContext();
            foreach (var item in todoContext.TodoItems)
            {
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task<TodoItem> AddTodoItem(AddTodoItemRequest request, ServerCallContext context)
        {
            var todoContext = GetScopedTodoContext();
            var item = request.ToItem();
            await todoContext.TodoItems.AddAsync(item);
            await todoContext.SaveChangesAsync();

            return await Task.FromResult(item);
        }

        public override async Task<TodoItem> UpdateTodoItem(UpdateTodoItemRequest request, ServerCallContext context)
        {
            var todoContext = GetScopedTodoContext();
            var item = await todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (item == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo Item not found"));
            }

            item.Name = request.Name;
            item.IsComplete = request.IsComplete;

            todoContext.TodoItems.Update(item);
            await todoContext.SaveChangesAsync();
            return await Task.FromResult(item);
        }

        public override async Task<Empty> DeleteTodoItem(DeleteTodoItemRequest request, ServerCallContext context)
        {
            var todoContext = GetScopedTodoContext();
            var item = await todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (item == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo Item not found"));
            }

            todoContext.TodoItems.Remove(item);
            await todoContext.SaveChangesAsync();
            return await Task.FromResult(new Empty());
        }

        private TodoContext GetScopedTodoContext()
        {
            var scoped = _provider.CreateScope();
            var context = scoped.ServiceProvider.GetRequiredService<TodoContext>();
            Console.WriteLine(context.Id);
            return context;
        }
    }
}