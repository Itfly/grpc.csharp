namespace Grpc.Retry
{
    using Core;

    public abstract class RetryPolicy
    {
        private BackoffPolicy backoffPolicy;

        protected RetryPolicy()
        {
            this.backoffPolicy = new ExponentialBackoffPolicy();
        }

        protected RetryPolicy(BackoffPolicy backoffPolicy)
        {
            this.backoffPolicy = backoffPolicy;
        }

        // MethodDescriptor
        public abstract bool IsRetryable(Status status, CallOptions callOptions);
    }
}