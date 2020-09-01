using System.Net;
using System.Reflection;
using MooMed.Common.Definitions.Configuration;
using MooMed.Logging.Loggers;
using MooMed.Logging.LogManagement.Interface;

namespace MooMed.Logging.LogManagement
{
	public abstract class AbstractLogFileManager : ILogFileManager
	{
		protected string LogFilePath { get; }

		protected AbstractLogFileManager(IConfigProvider configProvider)
		{
			LogFilePath = configProvider.ReadValue("Logging_File_Path", LoggerConstants.Logging_File_Path);
		}

		protected string GetServiceName() => Assembly.GetEntryAssembly()?.GetName().Name ?? Dns.GetHostName();

		public abstract string GetFilePath();
	}
}