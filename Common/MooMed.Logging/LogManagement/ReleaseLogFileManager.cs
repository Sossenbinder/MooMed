using MooMed.Common.Definitions.Configuration;

namespace MooMed.Logging.LogManagement
{
	internal class ReleaseLogFileManager : AbstractLogFileManager
	{
		public ReleaseLogFileManager(
			IConfigSettingsProvider configSettingsProvider)
			:base(configSettingsProvider)
		{
		}

		public override string GetFilePath() => $"{LogFilePath}{GetServiceName()}";
	}
}
