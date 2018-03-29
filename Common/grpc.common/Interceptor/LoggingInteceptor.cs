namespace Grpc.Common.Interceptor
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Common.Extension;
    using Grpc.Core;
    using Grpc.Core.Interceptors;
    using Grpc.Core.Logging;

    public class LoggingInterceptor : Interceptor
    {
        private static readonly ILogger _logger = GrpcEnvironment.Logger.ForType<LoggingInterceptor>();

        private static readonly string _logFormat = "{Duration} [{Method} {Status}] [{RequestId} {CorrelationId}]";

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation) =>
            ContinueWithLog(context, continuation(request, context));

        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation) =>
            ContinueWithLog(context, continuation(requestStream, context));

        public override Task ServerStreamingServerHandler<TRequest, TResponse>(
            TRequest request,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            ServerStreamingServerMethod<TRequest, TResponse> continuation) =>
            ContinueWithLog(context, continuation(request, responseStream, context));

        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            DuplexStreamingServerMethod<TRequest, TResponse> continuation) =>
            ContinueWithLog(context, continuation(requestStream, responseStream, context));

        private async Task<R> ContinueWithLog<R>(ServerCallContext context, Task<R> continuationTask)
        {
            var startTime = DateTime.Now;
            try
            {
                return await continuationTask;
            }
            finally
            {
                var (requestId, correlationId) = GetIds(context);
                var duration = DateTime.UtcNow - startTime;

                _logger.Info(_logFormat,
                    duration.Ticks,
                    context.Method, context.Status.ToString(),
                    requestId, correlationId);
            }
        }

        private async Task ContinueWithLog(ServerCallContext context, Task continuationTask)
        {
            var startTime = DateTime.UtcNow;
            try
            {
                await continuationTask;
            }
            finally
            {
                var (requestId, correlationId) = GetIds(context);
                var duration = DateTime.UtcNow - startTime;

                _logger.Info("duration: " + duration.Ticks);

                _logger.Info(_logFormat,
                    duration.Ticks,
                    context.Method, context.Status.ToString(),
                    requestId, correlationId);
            }
        }

        private (string requestId, string correlationId) GetIds(ServerCallContext context)
        {
            var metadata = context.RequestHeaders;
            // check null?
            if (metadata == null)
            {
                _logger.Error("Metadata is null");
            }
            var requestId = Guid.NewGuid().ToString("N");
            metadata.AddRequestId(requestId);
            var correlationId = GetOrCreateCorrelationId(metadata);

            return (requestId, correlationId);
        }

        private string GetOrCreateCorrelationId(Metadata metadata)
        {
            var correlationId = metadata.GetCorrelationId();
            if (correlationId == null)
            {
                correlationId = Guid.NewGuid().ToString("N");
                metadata.AddCorrelationId(correlationId);
            }

            return correlationId;
        }
    }
}