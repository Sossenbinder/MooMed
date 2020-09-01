using MooMed.Common.Definitions.Configuration;

namespace MooMed.Logging.LogManagement
{
	internal class ReleaseLogFileManager : AbstractLogFileManager
	{
		public ReleaseLogFileManager(
			IConfigProvider configProvider)
			: base(configProvider)
		{
		}

		public override string GetFilePath() => $"{LogFilePath}{GetServiceName()}";
	}
}