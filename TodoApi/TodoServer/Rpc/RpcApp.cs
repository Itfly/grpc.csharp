namespace TodoServer
{
    using System;
    using System.Threading.Tasks;

    using Grpc.Common.Interceptor;
    using Grpc.Core;
    using Grpc.Core.Interceptors;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Todo.Proto;
    using TodoServer.Service;

    public class RpcApp
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<RpcApp> _logger;
        private readonly IConfigurationRoot _config;

        private Server _server;

        /// <summary>
        /// Initiate the App by IServiceProvider.
        /// Beceause the GrpcServer do not support DI, we have to pass throught the IServiceProvider
        /// </summary>
        /// <param name="provider"></param>
        public RpcApp(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _logger = _provider.GetRequiredService<ILoggerFactory>().CreateLogger<RpcApp>();
            _config = _provider.GetService<IConfigurationRoot>();
        }

        public void Start()
        {
            var host = _config["Server:Host"];
            var port = int.Parse(_config["Server:Port"]);

            _server = new Server
            {
                Services = { TodoApi.BindService(new RpcHandler(_provider)).Intercept(new LoggingInterceptor()) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            _server.Start();
            _logger.LogInformation($"{Todo.Constant.EndpointName} server listening on port " + port);
        }

        public async Task ShutdownAsync() => await _server.ShutdownAsync();
    }
}

