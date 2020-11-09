using System.IO;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Logging.LogManagement
{
	internal class DebugLogFileManager : AbstractLogFileManager
	{
		public DebugLogFileManager(IConfigProvider configProvider)
			: base(configProvider)
		{
		}

		public override string GetFilePath()
		{
			// Get the base path, e.g. /hostlogs/ProfilePictureService
			var serviceLogsPath = $"/{LogFilePath}{GetServiceName()}";

			PrepareEnv(serviceLogsPath);

			return $"{serviceLogsPath}/{GetServiceName()}.txt";
		}
	}
}