namespace PingClient
{
    using System;
    using Grpc.Core;
    using Ping.Proto;

    public class PingClient
    {
        private readonly PingApi.PingApiClient _client;

        public PingClient(PingApi.PingApiClient client)
        {
            _client = client;
        }

        public void Ping(string sender, long ttl)
        {
            var ping = new Ping
            {
                Sender = sender,
                Timestamp = DateTime.UtcNow.Ticks,
                Ttl = ttl
            };

            try
            {
                Console.WriteLine("Ping...");

                var pong = _client.Echo(ping);
                Console.WriteLine($"Ping reply status: {pong.IsSuccess}");
            }
            catch (RpcException e)
            {
                Console.WriteLine("Rpc failed, " + e.Message);
                throw;
            }
        }
    }
}