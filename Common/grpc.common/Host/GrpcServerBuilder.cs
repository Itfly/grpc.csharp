using System;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Grpc.Common.Rpc.Host
{
    public class GrpcServerBuilder
    {
        private ServerServiceDefinition _serviceDefinition;
        private string _host;
        private int _port;
        private Interceptor[] _interceptors;

        // TODO: support credentials

        public GrpcServerBuilder SetConfiguration(IConfiguration configuration)
        {
            _host = configuration["Server:Host"];
            _port = int.Parse(configuration["Server:Port"]);
            // TODO: read interceptors and other configs?
            return this;
        }

        public GrpcServerBuilder SetHost(string host)
        {
            _host = host;
            return this;
        }

        public GrpcServerBuilder SetPort(int port)
        {
            _port = port;
            return this;
        }

        /// <summary>
        /// For Intercept(a, b, c), the order of invocation will be "a", "b", and then "c".
        /// building a chain like "serverServiceDefinition.Intercept(c).Intercept(b).Intercept(a)".  Note that
        /// in this case, the last interceptor added will be the first to take control.
        /// </summary>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        public GrpcServerBuilder AddInterceptor(params Interceptor[] interceptors)
        {
            _interceptors = interceptors;
            return this;
        }

        internal GrpcServerBuilder(ServerServiceDefinition serviceDefinition)
        {
            _serviceDefinition = serviceDefinition;
        }


        internal Server Build()
        {
            if (string.IsNullOrEmpty(_host))
            {
                throw new ArgumentNullException("host can not be null");
            }
            if (_port <= 0)
            {
                throw new ArgumentNullException("port can not be zero");
            }

            if (_interceptors != null && _interceptors.Length > 0)
            {
                _serviceDefinition = _serviceDefinition.Intercept(_interceptors);
            }

            // Add the default server-side interceptors
            _serviceDefinition =  _serviceDefinition.Intercept(new ExceptionHandleInterceptor());
            _serviceDefinition = _serviceDefinition.Intercept(new RequestLogInterceptor());

            return new Server
            {
                Services = { _serviceDefinition },
                Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) }
            };
        }
    }
}
