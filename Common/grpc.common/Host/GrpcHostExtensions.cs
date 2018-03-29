using System;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Grpc.Common.Rpc.Host
{
    public static class GrpcHostExtensions
    {
        public static IServiceCollection AddGrpcServer(
            this IServiceCollection serviceCollection,
            Action<GrpcServerBuilder> setBuilder,
            Func<IServiceProvider, ServerServiceDefinition> serviceDefinitionCreator)
        {
            serviceCollection.AddSingleton(provider => {
                // Find a better place to enable gRPC logging
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                loggerFactory.LogGrpc();

                serviceDefinitionCreator.Invoke(provider);
                var builder = new GrpcServerBuilder(serviceDefinitionCreator(provider));
                setBuilder(builder);
                return builder.Build();
            });

            serviceCollection.AddSingleton<IHostedService, GrpcHostService>();

            return serviceCollection;
        }
    }
}
