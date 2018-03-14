namespace Grpc.Common.Logging
{
    using System;
    using IGrpcLogger = Grpc.Core.Logging.ILogger;
    using IMsLogger = Microsoft.Extensions.Logging.ILogger;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Microsoft.Extensions.Logger's Logger implementation for Grpc.Core.Logging.ILogger
    /// </summary>
    internal class MicrosoftLogger : IGrpcLogger
    {
        private IMsLogger _logger;
        private ILoggerFactory _loggerFactory;

        public MicrosoftLogger(IMsLogger logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        public IGrpcLogger ForType<T>()
        {
            return new MicrosoftLogger(_loggerFactory.CreateLogger(typeof(T).FullName), _loggerFactory);
        }

        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }

        public void Debug(string format, params object[] formatArgs)
        {
            _logger.LogDebug(format, formatArgs);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Info(string format, params object[] formatArgs)
        {
            _logger.LogInformation(format, formatArgs);
        }

        public void Warning(string message)
        {
            _logger.LogWarning(message);
        }

        public void Warning(string format, params object[] formatArgs)
        {
            _logger.LogWarning(format, formatArgs);
        }

        public void Warning(Exception exception, string message)
        {
            _logger.LogWarning(exception, message);
        }

        void IGrpcLogger.Error(string message)
        {
            _logger.LogError(message);
        }

        public void Error(string format, params object[] formatArgs)
        {
            _logger.LogError(format, formatArgs);
        }

        public void Error(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }
    }
}
