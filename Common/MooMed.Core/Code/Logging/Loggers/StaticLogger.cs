using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Core.Code.Logging.Loggers
{
	public class StaticLogger
	{
		[NotNull]
		private static IMainLogger m_logger;

		public StaticLogger([NotNull] IMainLogger logger)
		{
			m_logger = logger;
		}

		public static void Info([NotNull] string message) => m_logger.Info(message);

		public static void Info([NotNull] string message, int accountId) => m_logger.Info(message, accountId);

		public static void Info([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> m_logger.Info(message, sessionContext);

		public static void Debug([NotNull] string message) => m_logger.Debug(message);

		public static void Debug([NotNull] string message, int accountId) => m_logger.Debug(message, accountId);

		public static void Debug([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> m_logger.Debug(message, sessionContext);

		public static void Warning([NotNull] string message) => m_logger.Warning(message);

		public static void Warning([NotNull] string message, int accountId) => m_logger.Warning(message, accountId);

		public static void Warning([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> m_logger.Warning(message, sessionContext);

		public static void Error([NotNull] string message) => m_logger.Error(message);

		public static void Error([NotNull] string message, int accountId) => m_logger.Error(message, accountId);

		public static void Error([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> m_logger.Error(message, sessionContext);

		public static void Fatal([NotNull] string message) => m_logger.Fatal(message);

		public static void Fatal([NotNull] string message, int accountId) => m_logger.Fatal(message, accountId);

		public static void Fatal([NotNull] string message, [CanBeNull] ISessionContext sessionContext)
			=> m_logger.Fatal(message, sessionContext);
	}
}
