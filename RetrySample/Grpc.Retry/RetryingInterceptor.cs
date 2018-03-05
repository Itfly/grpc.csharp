namespace Grpc.Retry
{
    using Core;
    using Core.Interceptors;
    public class RetryingInterceptor : Interceptor
    {
        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                return continuation(request, context);
            }
            catch (RpcException e)
            {
                return BlockingUnaryCall<TRequest, TResponse>(request, context, continuation);
            }
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                return continuation(request, context);
            }
            catch (RpcException e)
            {
                return AsyncUnaryCall<TRequest, TResponse>(request, context, continuation);
            }
        }
    }
}