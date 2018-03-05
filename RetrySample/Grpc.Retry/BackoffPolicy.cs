namespace Grpc.Retry
{
    /// <summary>
    /// Determines how long to wait before doing some action (typically a retry, or a reconnect).
    /// </summary>
    public interface BackoffPolicy
    {
        /// <summary>
        /// The number of nanoseconds to wait.
        /// </summary>
        /// <returns></returns>
        long NextBackoffNanos();
    }
}