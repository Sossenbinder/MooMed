using System;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.Code.Logging.LogManagement.Interface;
using Serilog;
using Serilog.Context;

namespace MooMed.Core.Code.Logging.Loggers
{
	public class MainTableStorageLogger : IMainLogger
	{
		private readonly ILogger m_logger;

		public MainTableStorageLogger([NotNull] IConfigSettingsProvider configSettingsProvider)
		{
			var connectionString = configSettingsProvider.ReadDecryptedValueOrFail<string>("MooMed_Logging_TableStorageConnectionString", "AccountKey");
			var storage = CloudStorageAccount.Parse(connectionString);
			
			m_logger = Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo
				.Async(logConfig =>
				{
					logConfig.Console();
					logConfig.AzureTableStorage(storage);
				})
				.CreateLogger();
		}

		public void Info(string message, ISessionContext sessionContext) => LogWithSessionContext(message, m_logger.Information, sessionContext);

		public void Warning(string message, ISessionContext sessionContext) => LogWithSessionContext(message, m_logger.Warning, sessionContext);

		public void Error(string message, ISessionContext sessionContext) => LogWithSessionContext(message, m_logger.Error, sessionContext);

		public void Fatal(string message, ISessionContext sessionContext) => LogWithSessionContext(message, m_logger.Fatal, sessionContext);

		public void System(string message, ISessionContext sessionContext) => LogWithSessionContext(message, m_logger.Debug, sessionContext);

		private void LogWithSessionContext(
			[NotNull] string message, 
			[NotNull] Action<string, string> logAction,
			[CanBeNull] ISessionContext sessionContext)
		{
			using (LogContext.PushProperty("SessionContext", sessionContext))
			{
				logAction("{message}", message);
			}
		}
	}
}
