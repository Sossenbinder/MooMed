using System;
using System.IO;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Logging.LogManagement
{
	internal class DebugLogFileManager : AbstractLogFileManager
	{
		public DebugLogFileManager(IConfigSettingsProvider configSettingsProvider)
			: base(configSettingsProvider)
		{
		}

		public override string GetFilePath()
		{
			// Get the base path, e.g. /hostlogs/ProfilePictureService
			var serviceLogsPath = $"/{LogFilePath}{GetServiceName()}";

			PrepareEnv(serviceLogsPath);

			return $"{serviceLogsPath}/{GetServiceName()}.txt";
		}

		private void PrepareEnv(string serviceLogsPath)
		{
			if (!Directory.Exists(serviceLogsPath))
			{
				Directory.CreateDirectory(serviceLogsPath);
			}
		}
	}
}
