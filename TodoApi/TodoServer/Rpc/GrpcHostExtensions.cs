using Grpc.Common.Logging;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace TodoServer.Rpc
{
    public static class GrpcHostExtensions
    {
        public static IServiceCollection AddGrpcServer(
            this IServiceCollection serviceCollection,
            Action<GrpcServerBuilder> serverConfigurator,
            Func<IServiceProvider, ServerServiceDefinition> serviceDefinitionCreator
            )
        {
            serviceCollection.AddSingleton(provider => {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                loggerFactory.LogGrpc();

                var builder = new GrpcServerBuilder(() => serviceDefinitionCreator(provider));
                serverConfigurator(builder);
                return builder.Build();
                }
            );
            serviceCollection.AddSingleton<IHostedService, GrpcHostService>();
            return serviceCollection;
        }
    }
}
