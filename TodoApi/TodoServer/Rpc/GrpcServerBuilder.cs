using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoServer.Rpc
{
    public class GrpcServerBuilder
    {
        private Func<ServerServiceDefinition> _serviceDefinitionCreator;

        public ServerServiceDefinition Service { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public IList<Interceptor> Interceptors { get; }

        // TODO: add ServerCredentials

        public GrpcServerBuilder(Func<ServerServiceDefinition> serviceDefinitionCreator)
        {
            _serviceDefinitionCreator = serviceDefinitionCreator;
            Interceptors = new List<Interceptor>();
        }

        public GrpcServerBuilder SetConfig(IConfiguration configuration)
        {
            Host = configuration["Server:Host"];
            Port = int.Parse(configuration["Server:Port"]);
            return this;
        }

        public GrpcServerBuilder SetHost(string host)
        {
            Host = host;
            return this;
        }

        public GrpcServerBuilder SetPort(int port)
        {
            Port = port;
            return this;
        }


        public GrpcServerBuilder AddInterceptor(Interceptor interceptor)
        {
            Interceptors.Add(interceptor);
            return this;
        }

        public Server Build()
        {
            return new Server
            {
                Services = { _serviceDefinitionCreator().Intercept(Interceptors.ToArray()) },
                Ports = { new ServerPort(Host, Port, ServerCredentials.Insecure) }
            };

        }
    }
}
