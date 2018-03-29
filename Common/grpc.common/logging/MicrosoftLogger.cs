namespace Grpc.Common.Logging
{
    using System;
    using IGrpcLogger = Grpc.Core.Logging.ILogger;
    using IMsLogger = Microsoft.Extensions.Logging.ILogger;
    using Microsoft.Extensions.Logging;
    using Grpc.Common.Interceptor;

    /// <summary>
    /// Microsoft.Extensions.Logger's Logger implementation for Grpc.Core.Logging.ILogger
    /// </summary>
    internal class MicrosoftLogger : IGrpcLogger
    {
        private IMsLogger _logger;
        private ILoggerFactory _loggerFactory;
        private int _eventId;

        public MicrosoftLogger(IMsLogger logger, ILoggerFactory loggerFactory, int eventId = LoggingEvents.GrpcCore)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _eventId = eventId;
        }

        public IGrpcLogger ForType<T>()
        {
            Type type = typeof(T);
            var eventId = GetEventId(type);
            return new MicrosoftLogger(_loggerFactory.CreateLogger(type.FullName), _loggerFactory, eventId);
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
            _logger.LogInformation(_eventId, format, formatArgs);
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

        public void Error(string message)
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

        private int GetEventId(Type type)
        {
            if (type == typeof(LoggingInterceptor))
            {
                return LoggingEvents.RequestLog;
            }

            return _eventId;
        }
    }
}
