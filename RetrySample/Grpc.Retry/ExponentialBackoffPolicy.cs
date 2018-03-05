namespace Grpc.Retry
{
    using System;

    public class ExponentialBackoffPolicy : BackoffPolicy
    {
        private Random _random = new Random();
        private static readonly long initialBackoffNanos = TimeSpan.TicksPerSecond;
        private static readonly long maxBackoffNanos = TimeSpan.TicksPerMinute;
        private static readonly double multiplier = 1.6;
        private static readonly double jitter = .2;

        private long nextBackoffNanos = initialBackoffNanos;

        public long NextBackoffNanos()
        {
            long currentBackoffNanos = nextBackoffNanos;
            nextBackoffNanos = Math.Min((long) (currentBackoffNanos * multiplier), maxBackoffNanos);
            return currentBackoffNanos 
                   + UniformRandom(-jitter * currentBackoffNanos, jitter * currentBackoffNanos);
        }

        private long UniformRandom(double low, double high)
        {
            if (low > high)
            {
                throw new ArgumentException();
            }

            double mag = high - low;
            return (long) (_random.NextDouble() * mag + low);
        }
    }
}