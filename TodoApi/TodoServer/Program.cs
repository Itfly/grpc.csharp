namespace TodoServer
{
    using System;
    using System.Threading;

    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Todo.Proto;

    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("todo_db"), ServiceLifetime.Singleton/* only because it's InMemory */)
                //.AddLogging()
                .BuildServiceProvider();

            var server = new Server
            {
                Services = { TodoApi.BindService(new TodoApiImpl(serviceProvider)) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Todo API server listening on port " + Port);
            Console.WriteLine("Press ctrl+c to stop the server...");

            var resetEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                Console.WriteLine("Receive terminal signal.");
                resetEvent.Set();
            };


            resetEvent.WaitOne();

            server.ShutdownAsync().Wait();
            serviceProvider.Dispose();

            Console.WriteLine("Server stopped");
        }
    }
}
