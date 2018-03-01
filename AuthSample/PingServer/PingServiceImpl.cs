namespace PingServer
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Ping.Proto;
    public class PingServiceImpl : PingApi.PingApiBase
    {
        public override async Task<Pong> Echo(Ping request, ServerCallContext context)
        {
            var now = DateTime.UtcNow.Ticks;
            Console.WriteLine($"Receive ping from {request.Sender} at {request.Timestamp} and now is {now}");

            return await Task.FromResult(new Pong
            {
                IsSuccess = (now - request.Timestamp) < request.Ttl,
                Timestamp = now
            });
        }
    }
}