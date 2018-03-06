namespace Routeguide
{
    using System;
    using Grpc.Core.Logging;
    public class SerilogGrpcLogger : ILogger
    {
        private Serilog.ILogger _logger;

        public SerilogGrpcLogger(Serilog.ILogger log)
        {
            _logger = log;
        }

        public ILogger ForType<T>()
        {
            return new SerilogGrpcLogger(_logger.ForContext<T>());
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string format, params object[] formatArgs)
        {
            _logger.Debug(format, formatArgs);
        }

        public void Info(string message)
        {
            _logger.Information(message);
        }

        public void Info(string format, params object[] formatArgs)
        {
            _logger.Information(format, formatArgs);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Warning(string format, params object[] formatArgs)
        {
            _logger.Warning(format, formatArgs);
        }

        public void Warning(Exception exception, string message)
        {
            _logger.Warning(exception, message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string format, params object[] formatArgs)
        {
            _logger.Error(format, formatArgs);
        }

        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }
    }
}