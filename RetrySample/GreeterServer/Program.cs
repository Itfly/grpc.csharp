namespace GreeterServer
{
using System;
using System.Threading.Tasks;
using Greeter.Proto;
using Grpc.Core;
using Grpc.Core.Logging;
using Grpc.Core.Utils;

    class GreeterImpl : Greeter.GreeterBase
    {
        // Server side handler of the SayHello RPC
        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
            {
                Console.WriteLine("Error!");
                throw new RpcException(new Status(StatusCode.Cancelled, ""));
            }

            Console.WriteLine("Hello!");
            return await Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }

    class Program
    {
        const int Port = 50051;

        public static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { Greeter.BindService(new GreeterImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}