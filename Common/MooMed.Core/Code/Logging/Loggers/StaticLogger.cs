using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Core.Code.Logging.Loggers
{
	public class StaticLogger
	{
		[NotNull]
		private static IMainLogger _logger;

		public StaticLogger([NotNull] IMainLogger logger)
		{
			_logger = logger;
		}

		public static void Info([NotNull] string message) => _logger.Info(message);

		public static void Info([NotNull] string message, int accountId) => _logger.Info(message, accountId);

		public static void Info([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> _logger.Info(message, sessionContext);

		public static void Debug([NotNull] string message) => _logger.Debug(message);

		public static void Debug([NotNull] string message, int accountId) => _logger.Debug(message, accountId);

		public static void Debug([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> _logger.Debug(message, sessionContext);

		public static void Warning([NotNull] string message) => _logger.Warning(message);

		public static void Warning([NotNull] string message, int accountId) => _logger.Warning(message, accountId);

		public static void Warning([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> _logger.Warning(message, sessionContext);

		public static void Error([NotNull] string message) => _logger.Error(message);

		public static void Error([NotNull] string message, int accountId) => _logger.Error(message, accountId);

		public static void Error([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> _logger.Error(message, sessionContext);

		public static void Fatal([NotNull] string message) => _logger.Fatal(message);

		public static void Fatal([NotNull] string message, int accountId) => _logger.Fatal(message, accountId);

		public static void Fatal([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> _logger.Fatal(message, sessionContext);
	}
}
