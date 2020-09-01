using System.Diagnostics.CodeAnalysis;
using Microsoft.WindowsAzure.Storage;
using MooMed.Common.Definitions.Configuration;
using MooMed.Configuration.Interface;
using MooMed.Logging.LogManagement.Interface;
using Serilog;
using Serilog.Configuration;

namespace MooMed.Logging.Loggers.Helper
{
	public class SerilogConfigProvider
	{
		private readonly IConfigProvider _configProvider;

		private readonly ILogFileManager _logfileManager;

		public SerilogConfigProvider(
			IConfigProvider configProvider,
			ILogFileManager logfileManager)
		{
			_configProvider = configProvider;
			_logfileManager = logfileManager;
		}

		public LoggerConfiguration CreateConfig()
		{
			return new LoggerConfiguration()
				.Enrich
				.FromLogContext()
				.WriteTo
				.Async(logConfig =>
				{
					logConfig.Console();

					PrepareTableStorageLogConfig(logConfig);

					PrepareFileLogConfig(logConfig);
				});
		}

		private void PrepareTableStorageLogConfig([NotNull] LoggerSinkConfiguration loggerSinkConfig)
		{
			var connectionString = _configProvider.ReadDecryptedValueOrFail<string>("MooMed_Logging_TableStorageConnectionString", "AccountKey");
			var storage = CloudStorageAccount.Parse(connectionString);

			var storageTableName = _configProvider.ReadValue("Logging_AzureTableStorage_TableName", LoggerConstants.Logging_AzureTableStorage_TableName);

			loggerSinkConfig.AzureTableStorage(
				storage,
				storageTableName: storageTableName,
				writeInBatches: true);
		}

		private void PrepareFileLogConfig([NotNull] LoggerSinkConfiguration loggerSinkConfig)
		{
			var logPath = _logfileManager.GetFilePath();

			loggerSinkConfig.File(
				logPath,
				fileSizeLimitBytes: 524288000,
				rollingInterval: RollingInterval.Day);
		}
	}
}