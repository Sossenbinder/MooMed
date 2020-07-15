using System.Collections.Generic;

namespace MooMed.Logging.Loggers
{
	public static class LoggerConstants
	{
		public static string Logging_AzureTableStorage_TableName = "Logs";

		public static string Logging_File_Path = "hostlogs/";

		public static Dictionary<string, string> GetConstantsAsInMemoryDict()
		{
			return new Dictionary<string, string>()
			{
				{ nameof(Logging_AzureTableStorage_TableName), Logging_AzureTableStorage_TableName },
				{ nameof(Logging_File_Path), Logging_File_Path }
			};
		}
	}
}
