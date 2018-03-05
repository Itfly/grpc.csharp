namespace GreeterClient
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Greeter.Proto;
    using Grpc.Core;
    using Grpc.Core.Interceptors;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            var callInvoker = channel.Intercept(new RetryingInterceptor());
            var client = new Greeter.GreeterClient(callInvoker);

            String user = "you";

            //int i = 0;
            //while (i < 10)
            //{
            //    var reply = client.SayHello(new HelloRequest { Name = user });
            //    Console.WriteLine("Greeting: " + reply.Message);
            //    i++;
            //}

            int i = 0;
            var tasks = new List<Task>();
            while (i < 10)
            {
                var ii = i;
                var task = Task.Run(async () =>
                    {
                        try
                        {
                            var reply = await client.SayHelloAsync(new HelloRequest {Name = user});
                            Console.WriteLine($"Greeting {ii}: " + reply.Message);
                        }
                        catch (RpcException e)
                        {
                            // Replace it with Polly's Fallback 
                            Console.WriteLine("Still got exception after retry, " + e.Message);
                        }
                    }
                );
                tasks.Add(task);
                i++;
            }
            Task.WaitAll(tasks.ToArray());

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
