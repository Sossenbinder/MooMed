using Serilog;

namespace MooMed.Logging.Loggers.Helper.Interface
{
	public interface ISerilogConfigProvider
	{
		LoggerConfiguration CreateConfig();
	}
}