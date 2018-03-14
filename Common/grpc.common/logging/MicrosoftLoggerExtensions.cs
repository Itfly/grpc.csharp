namespace Grpc.Common.Logging
{
    using Grpc.Core;
    using Microsoft.Extensions.Logging;
    using System;

    /// <summary>
    /// Let the Microsoft.Extensions.Logging to process the grpc log
    /// </summary>
    public static class MicrosoftLoggerExtensions
    {
        public static ILoggerFactory LogGrpc(
            this ILoggerFactory factory, 
            ILogger logger = null)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            if (logger == null)
            {
                logger = factory.CreateLogger("Grpc.Core");
            }

            GrpcEnvironment.SetLogger(new MicrosoftLogger(logger, factory));

            return factory;
        }
    }
}
