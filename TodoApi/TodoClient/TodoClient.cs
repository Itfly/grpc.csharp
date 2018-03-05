namespace TodoClient
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Todo.Proto;

    public class TodoClient
    {
        private readonly TodoApi.TodoApiClient _client;

        public TodoClient(TodoApi.TodoApiClient client)
        {
            _client = client;
        }

        public void GetTodoItem(long id)
        {
            try
            {
                Log($"*** GetTodoItem: id={id} ***");

                var request = new GetTodoItemRequest
                {
                    Id = id
                };

                var item = _client.GetTodoItem(request);
                Log($"Found TodoItem: id={item.Id}, name={item.Name}, isComplete={item.IsComplete}");
            }
            catch (RpcException e)
            {
                if (e.Status.StatusCode == StatusCode.NotFound)
                {
                    Log($"Found no TodoItem for id={id}");
                    return;
                }

                Log("Rpc failed, " + e.Message);
                throw;
            }
        }

        public async Task GetAllTodoItems(CancellationToken token)
        {
            try
            {
                Log($"*** GetAllTodoItems ***");

                using (var call = _client.GetAllTodoItems(new Empty()))
                {
                    var responseStream = call.ResponseStream;
                    var responseLog = new StringBuilder("Result: ");

                    while (await responseStream.MoveNext(token))
                    {
                        var item = responseStream.Current;
                        responseLog.Append(item.ToString());
                    }
                    Log(responseLog.ToString());
                }
            }
            catch (RpcException e)
            {
                Log("Rpc failed, " + e.Message);
                throw;
            }
        }

        public void AddTodoItem(string name, bool isComplete)
        {
            try
            {
                Log($"*** AddTodoItem: name={name}, isComplete={isComplete} ***");

                var request = new AddTodoItemRequest
                {
                    Name = name,
                    IsComplete = isComplete
                };

                var item = _client.AddTodoItem(request);
                Log(item != null
                    ? $"Add TodoItem succeeded: id={item.Id}, name={item.Name}, isComplete={item.IsComplete}"
                    : $"Failed to add TodoItem");
            }
            catch (RpcException e)
            {
                Log("Rpc failed, " + e.Message);
                throw;
            }
        }

        public void UpdateTodoItem(TodoItem item)
        {
            try
            {
                Log($"*** UpdateTodoItem: id={item.Id}, name={item.Name}, isComplete={item.IsComplete} ***");

                var request = new UpdateTodoItemRequest
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsComplete = item.IsComplete
                };

                 _client.UpdateTodoItem(request);
            }
            catch (RpcException e)
            {
                if (e.Status.StatusCode == StatusCode.NotFound)
                {
                    Log($"Found no TodoItem");
                    return;
                }

                Log("Rpc failed, " + e.Message);
                throw;
            }
        }

        public void DeleteTodoItem(long id)
        {
            try
            {
                Log($"*** DeleteTodoItem: id={id} ***");

                var request = new DeleteTodoItemRequest
                {
                    Id = id,
                };

                _client.DeleteTodoItem(request);
            }
            catch (RpcException e)
            {
                if (e.Status.StatusCode == StatusCode.NotFound)
                {
                    Log($"Found no TodoItem");
                    return;
                }

                Log("Rpc failed, " + e.Message);
                throw;
            }
        }

        private static void Log(string s)
        {
            Console.WriteLine(s);
        }
  }
}