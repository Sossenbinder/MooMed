using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
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
					logConfig.AzureTableStorage(storage, storageTableName: "Logs");
				})
				.CreateLogger();
		}

		public void Info(string message)
			=> LogWithOutSessionContext(message, m_logger.Information);

		public void Info(string message, int accountId)
			=> LogWithAccountId(message, m_logger.Information, accountId);

		public void Info(string message, ISessionContext sessionContext)
			=> LogWithSessionContext(message, m_logger.Information, sessionContext);

		public void Debug(string message)
			=> LogWithOutSessionContext(message, m_logger.Debug);

		public void Debug(string message, int accountId)
			=> LogWithAccountId(message, m_logger.Debug, accountId);

		public void Debug(string message, ISessionContext sessionContext)
			=> LogWithSessionContext(message, m_logger.Debug, sessionContext);

		public void Warning(string message)
			=> LogWithOutSessionContext(message, m_logger.Warning);

		public void Warning(string message, int accountId)
			=> LogWithAccountId(message, m_logger.Warning, accountId);

		public void Warning(string message, ISessionContext sessionContext)
			=> LogWithSessionContext(message, m_logger.Warning, sessionContext);

		public void Error(string message)
			=> LogWithOutSessionContext(message, m_logger.Error);

		public void Error(string message, int accountId)
			=> LogWithAccountId(message, m_logger.Error, accountId);

		public void Error(string message, ISessionContext sessionContext)
			=> LogWithSessionContext(message, m_logger.Error, sessionContext);

		public void Fatal(string message)
			=> LogWithOutSessionContext(message, m_logger.Fatal);

		public void Fatal(string message, int accountId)
			=> LogWithAccountId(message, m_logger.Fatal, accountId);

		public void Fatal(string message, ISessionContext sessionContext)
			=> LogWithSessionContext(message, m_logger.Fatal, sessionContext);

		private void LogWithOutSessionContext(
			[NotNull] string message,
			[NotNull] Action<string> logAction)
		{
			logAction(message);
		}

		private void LogWithAccountId(
			[NotNull] string message,
			[NotNull] Action<string, string> logAction,
			int accountId)
		{
			using (LogContext.PushProperty("AccountId", accountId))
			{
				logAction("{message}", message);
			}
		}

		private void LogWithSessionContext(
			[NotNull] string message,
			[NotNull] Action<string, string> logAction,
			[CanBeNull] ISessionContext sessionContext)
			=> LogWithAccountId(message, logAction, sessionContext?.Account.Id ?? 0);
	}
}
