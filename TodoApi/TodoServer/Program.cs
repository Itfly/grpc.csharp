using System;

namespace TodoServer
{
    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Todo.Proto;

    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("todo_db")
                .Options;
            var context = new TodoContext(options);

            var server = new Server
            {
                Services = { TodoApi.BindService(new TodoApiImpl(context)) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Todo API server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
            context.Dispose();
        }
    }
}
