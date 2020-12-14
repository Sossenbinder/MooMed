using MooMed.Logging.Loggers.Helper.Interface;
using Serilog;

namespace MooMed.TestBase.Utils
{
    public class UnitTestSerilogConfigProvider : ISerilogConfigProvider
    {
        public LoggerConfiguration CreateConfig()
        {
            return new LoggerConfiguration()
                .Enrich
                .FromLogContext()
                .WriteTo
                .Async(logConfig => logConfig.Console());
        }
    }
}