using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Configuration;
using MooMed.Logging.Loggers;
using MooMed.Logging.LogManagement.Interface;

namespace MooMed.Logging.LogManagement
{
	public abstract class AbstractLogFileManager : ILogFileManager
	{
		private readonly IConfigProvider _configProvider;

		protected string LogFilePath { get; }

		protected AbstractLogFileManager(IConfigProvider configProvider)
		{
			_configProvider = configProvider;
			LogFilePath = configProvider.ReadValue("Logging_File_Path", LoggerConstants.Logging_File_Path);

			Task.Run(CleanOldLogs);
		}

		public abstract string GetFilePath();

		protected static string GetServiceName() => Assembly.GetEntryAssembly()?.GetName().Name ?? Dns.GetHostName();

		protected static void PrepareEnv(string serviceLogsPath)
		{
			if (!Directory.Exists(serviceLogsPath))
			{
				Directory.CreateDirectory(serviceLogsPath);
			}
		}

		private void CleanOldLogs()
		{
			var serviceName = Assembly.GetEntryAssembly()?.GetName().Name;

			var maximumHistoryFileCount = _configProvider.ReadValue<int>("Logging_MaxHistoryCount");

			var path = $"{LogFilePath}/{serviceName}";

			if (!Directory.Exists(path))
			{
				return;
			}

			var existingFiles = Directory
				.GetFiles(path)
				.ToList();

			if (existingFiles.Count <= maximumHistoryFileCount)
			{
				return;
			}

			var orderedFiles = existingFiles
				.OrderBy(x => x)
				.Take(existingFiles.Count - maximumHistoryFileCount);

			foreach (var orderedFile in orderedFiles)
			{
				File.Delete(orderedFile);
			}
		}
	}
}