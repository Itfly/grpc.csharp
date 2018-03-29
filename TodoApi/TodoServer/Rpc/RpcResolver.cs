namespace TodoServer.Service
{
    using System.Threading.Tasks;

    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Todo;
    using Todo.Proto;

    public class RpcResolver : TodoApi.TodoApiBase
    {
        private ITodoService _todoService;

        public RpcResolver(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public override async Task<TodoItem> GetTodoItem(GetTodoItemRequest request, ServerCallContext context)
        {
            return await _todoService.GetAsync(request.Id);
        }

        public override async Task GetAllTodoItems(Empty request, IServerStreamWriter<TodoItem> responseStream,
            ServerCallContext context)
        {
            foreach (var item in _todoService.GetAll())
            {
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task<TodoItem> AddTodoItem(AddTodoItemRequest request, ServerCallContext context)
        {
            return await _todoService.AddAsync(request.ToItem());
        }

        public override async Task<TodoItem> UpdateTodoItem(UpdateTodoItemRequest request, ServerCallContext context)
        {
            return await _todoService.UpdateAsync(request.ToItem());
        }

        public override async Task<Empty> DeleteTodoItem(DeleteTodoItemRequest request, ServerCallContext context)
        {
            await _todoService.DeleteAsync(request.Id);
            return await Task.FromResult(new Empty());
        }

        /// https://github.com/grpc/grpc/issues/12478
    }
}