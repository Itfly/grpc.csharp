namespace GreeterClient
{
    using System;
    using Grpc.Core;
    using Grpc.Core.Interceptors;
    using Grpc.Core.Logging;
    using Polly;

    public class RetryingInterceptor : Interceptor
    {
        private static readonly ILogger Logger = GrpcEnvironment.Logger.ForType<RetryingInterceptor>();

        private Policy _retryPolicy;

        public RetryingInterceptor()
        {
            _retryPolicy = Policy.Handle<RpcException>().Retry(5,
                (exception, retryCount) =>
                {
                    //Logger.Warning("Retry with exception: " + exception.Message);
                    Console.WriteLine($"Retry {retryCount} with exception: " + exception.Message);
                });
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            return _retryPolicy.Execute(() => continuation(request, context));
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            return _retryPolicy.Execute(() => continuation(request, context));
        }
    }
}