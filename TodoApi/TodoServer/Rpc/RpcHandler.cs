namespace TodoServer.Service
{
    using System;
    using System.Threading.Tasks;

    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Todo;
    using Todo.Proto;
    using TodoServer.Storage;

    public class RpcHandler : TodoApi.TodoApiBase
    {
        private readonly IServiceProvider _provider;

        public RpcHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        public override async Task<TodoItem> GetTodoItem(GetTodoItemRequest request, ServerCallContext context)
        {
            var scoped = NewServiceScope();
            var todoService = scoped.ServiceProvider.GetRequiredService<ITodoService>();

            return await todoService.GetAsync(request.Id);
        }

        public override async Task GetAllTodoItems(Empty request, IServerStreamWriter<TodoItem> responseStream,
            ServerCallContext context)
        {
            var scoped = NewServiceScope();
            var todoService = scoped.ServiceProvider.GetRequiredService<ITodoService>();

            foreach (var item in todoService.GetAll())
            {
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task<TodoItem> AddTodoItem(AddTodoItemRequest request, ServerCallContext context)
        {
            var scoped = NewServiceScope();
            var todoService = scoped.ServiceProvider.GetRequiredService<ITodoService>();
            return await todoService.AddAsync(request.ToItem());
        }

        public override async Task<TodoItem> UpdateTodoItem(UpdateTodoItemRequest request, ServerCallContext context)
        {
            var scoped = NewServiceScope();
            var todoService = scoped.ServiceProvider.GetRequiredService<ITodoService>();
            return await todoService.UpdateAsync(request.ToItem());
        }

        public override async Task<Empty> DeleteTodoItem(DeleteTodoItemRequest request, ServerCallContext context)
        {
            var scoped = NewServiceScope();
            var todoService = scoped.ServiceProvider.GetRequiredService<ITodoService>();
            await todoService.DeleteAsync(request.Id);

            return await Task.FromResult(new Empty());
        }

        private IServiceScope NewServiceScope()
        {
            // https://github.com/grpc/grpc/issues/12478
            return _provider.CreateScope();
        }
    }
}