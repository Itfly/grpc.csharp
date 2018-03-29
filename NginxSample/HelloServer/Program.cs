namespace HelloServer
{
    using Grpc.Core;
    using Helloworld.Proto;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class HelloImpl : Hello.HelloBase
    {
        public override async Task<GreetResponse> Greeting(GreetRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new GreetResponse
            {
                Message = "Hello"
            });
        }
    }

    public class Program
    {
        const int Port = 30001;

        static void Main(string[] args)
        {
            var server = new Server
            {
                Services = { Hello.BindService(new HelloImpl()) },
                Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure) }
            };

            server.Start();

            Console.WriteLine("Hello server listening on port " + Port);
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
        }
    }
}
