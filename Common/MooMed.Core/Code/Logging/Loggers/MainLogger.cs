using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.Code.Logging.LogManagement.Interface;
using Serilog;
using Serilog.Context;
using ILogger = Serilog.ILogger;

namespace MooMed.Core.Code.Logging.Loggers
{
	public class MainTableStorageLogger : IMainLogger
	{
		private readonly ILogger m_logger;

		public MainTableStorageLogger(
			[NotNull] ILogPathProvider logPathProvider,
			[NotNull] IConfigSettingsProvider configSettingsProvider)
		{
			var connectionString = configSettingsProvider.ReadDecryptedValueOrFail<string>("MooMed_Logging_TableStorageConnectionString", "AccountKey");
			var storage = CloudStorageAccount.Parse(connectionString);
			
			m_logger = Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo.Async(logConfig =>
				{
					logConfig.AzureTableStorage(storage);
				}).CreateLogger();
		}

		public void Info(string message, ISessionContext sessionContext)
		{
			using (LogContext.PushProperty("SessionContext", sessionContext))
			{
				m_logger.Information("blabla");
				m_logger.Information("{message}", message);
			}
		}

		public void Warning(string message, ISessionContext sessionContext)
		{
			m_logger.Warning(message);
		}

		public void Error(string message, ISessionContext sessionContext)
		{
			m_logger.Error(message);
		}

		public void Fatal(string message, ISessionContext sessionContext)
		{
			m_logger.Fatal(message);
		}

		public void System(string message, ISessionContext sessionContext)
		{
			m_logger.Debug(message);
		}
	}
}
