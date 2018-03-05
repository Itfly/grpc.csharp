namespace TodoClient
{
    using System;
    using System.Threading;
    using Grpc.Core;
    using Todo.Proto;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            var client = new TodoClient(new TodoApi.TodoApiClient(channel));

            // looking for an existing item
            client.GetTodoItem(1L);

            // item missing
            client.GetTodoItem(1000L);

            // add item
            client.AddTodoItem("item2", true);
            client.AddTodoItem("item3", false);

            // get all items
            client.GetAllTodoItems(CancellationToken.None).Wait();

            client.UpdateTodoItem(new TodoItem()
            {
                Id = 1L,
                Name = "item111",
                IsComplete = true
            });
            client.GetTodoItem(1L);

            client.DeleteTodoItem(2L);
            client.GetAllTodoItems(CancellationToken.None).Wait();

            client.DeleteTodoItem(2000L);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
