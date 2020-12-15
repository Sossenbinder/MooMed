using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.Logging.Loggers
{
    public class StaticLogger
    {
        private static IMooMedLogger _logger;

        public StaticLogger(IMooMedLogger logger)
        {
            _logger = logger;
        }

        public static void Info(string message, int accountId) => _logger.Info(message, accountId);

        public static void Info(string message, [CanBeNull] ISessionContext sessionContext)
            => _logger.Info(message, sessionContext);

        public static void Debug(string message) => _logger.Debug(message);

        public static void Debug(string message, int accountId) => _logger.Debug(message, accountId);

        public static void Debug(string message, [CanBeNull] ISessionContext sessionContext)
            => _logger.Debug(message, sessionContext);

        public static void Warning(string message) => _logger.Warning(message);

        public static void Warning(string message, int accountId) => _logger.Warning(message, accountId);

        public static void Warning(string message, [CanBeNull] ISessionContext sessionContext)
            => _logger.Warning(message, sessionContext);

        public static void Error(string message) => _logger.Error(message);

        public static void Error(string message, int accountId) => _logger.Error(message, accountId);

        public static void Error(string message, [CanBeNull] ISessionContext sessionContext)
            => _logger.Error(message, sessionContext);

        public static void Fatal(string message) => _logger.Fatal(message);

        public static void Fatal(string message, int accountId) => _logger.Fatal(message, accountId);

        public static void Fatal(string message, [CanBeNull] ISessionContext sessionContext)
            => _logger.Fatal(message, sessionContext);
    }
}