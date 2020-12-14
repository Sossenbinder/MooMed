using System;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Logging.Loggers.Helper.Interface;
using Serilog;
using Serilog.Context;

namespace MooMed.Logging.Loggers
{
    /// <summary>
    /// Wrapper around ILogger to provide a default implementation for attaching session related information
    /// </summary>
    public class MooMedLogger : IMooMedLogger
    {
        private readonly ILogger _logger;

        public MooMedLogger(ISerilogConfigProvider serilogConfigProvider)
        {
            _logger = Log.Logger = serilogConfigProvider
                .CreateConfig()
                .CreateLogger();
        }

        public void Info(string message)
            => LogWithOutSessionContext(message, _logger.Information);

        public void Info(string message, int accountId)
            => LogWithAccountId(message, _logger.Information, accountId);

        public void Info(string message, ISessionContext? sessionContext)
            => LogWithSessionContext(message, _logger.Information, sessionContext);

        public void Debug(string message)
            => LogWithOutSessionContext(message, _logger.Debug);

        public void Debug(string message, int accountId)
            => LogWithAccountId(message, _logger.Debug, accountId);

        public void Debug(string message, ISessionContext? sessionContext)
            => LogWithSessionContext(message, _logger.Debug, sessionContext);

        public void Warning(string message)
            => LogWithOutSessionContext(message, _logger.Warning);

        public void Warning(string message, int accountId)
            => LogWithAccountId(message, _logger.Warning, accountId);

        public void Warning(string message, ISessionContext? sessionContext)
            => LogWithSessionContext(message, _logger.Warning, sessionContext);

        public void Error(string message)
            => LogWithOutSessionContext(message, _logger.Error);

        public void Error(string message, int accountId)
            => LogWithAccountId(message, _logger.Error, accountId);

        public void Error(string message, ISessionContext? sessionContext)
            => LogWithSessionContext(message, _logger.Error, sessionContext);

        public void Exception(Exception exception)
            => Error(exception.Message);

        public void Exception(Exception exception, int accountId)
            => Error(exception.Message, accountId);

        public void Exception(Exception exception, ISessionContext? sessionContext)
            => Error(exception.Message, sessionContext);

        public void Fatal(string message)
            => LogWithOutSessionContext(message, _logger.Fatal);

        public void Fatal(string message, int accountId)
            => LogWithAccountId(message, _logger.Fatal, accountId);

        public void Fatal(string message, ISessionContext? sessionContext)
            => LogWithSessionContext(message, _logger.Fatal, sessionContext);

        private static void LogWithOutSessionContext(
            string message,
            Action<string> logAction)
        {
            logAction(message);
        }

        private static void LogWithAccountId(
            string message,
            Action<string, string> logAction,
            int accountId)
        {
            using (LogContext.PushProperty("AccountId", accountId))
            {
                logAction("{message}", message);
            }
        }

        private static void LogWithSessionContext(
            string message,
            Action<string, string> logAction,
            ISessionContext? sessionContext)
        {
            LogWithAccountId(message, logAction, sessionContext?.Account.Id ?? 0);
        }
    }
}