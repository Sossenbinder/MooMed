using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Core.Code.Logging.Loggers
{
    public static class StaticLogger
    {
        [NotNull]
        private static IMainLogger MooMedLogger { get; set; } = new NullMooMedLogger();

        public static void Info(string message, [NotNull] SessionContext sessionContext)
			=> MooMedLogger.Info(message, sessionContext);

        public static void Warning(string message, [NotNull]  SessionContext sessionContext)
	        => MooMedLogger.Warning(message, sessionContext);

		public static void Error(string message, [NotNull]  SessionContext sessionContext)
			=> MooMedLogger.Error(message, sessionContext);

		public static void Fatal(string message, [NotNull]  SessionContext sessionContext)
			=> MooMedLogger.Fatal(message, sessionContext);

		public static void System(string message, [NotNull]  SessionContext sessionContext)
			=> MooMedLogger.System(message, sessionContext);
	}
}
