using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace Grpc.Common.Rpc.Host
{
    public class GrpcHostService : IHostedService
    {
        private readonly Server _server;

        public GrpcHostService(Server server)
        {
            _server = server;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _server.Start();
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await _server.ShutdownAsync();
    }
}
