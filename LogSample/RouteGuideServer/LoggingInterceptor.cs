namespace Routeguide
{
    using System.Threading.Tasks;
    using Grpc.Core;
    using Grpc.Core.Interceptors;
    using Grpc.Core.Logging;

    public class LoggingInterceptor : Interceptor
    {
        private static readonly ILogger Logger = GrpcEnvironment.Logger.ForType<LoggingInterceptor>();

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            Logger.Info($"Call {context.Method}");

            return continuation(request, context);
        }

        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            Logger.Info($"Call {context.Method}");

            return continuation(requestStream, context);
        }

        public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            Logger.Info($"Call {context.Method}");

            return continuation(request, responseStream, context);
        }

        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            Logger.Info($"Call {context.Method}");

            return continuation(requestStream, responseStream, context);
        }
    }
}