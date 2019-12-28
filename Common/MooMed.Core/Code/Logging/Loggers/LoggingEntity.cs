using Microsoft.WindowsAzure.Storage.Table;

namespace MooMed.Core.Code.Logging.Loggers
{
    public class LoggingEntity : TableEntity
    {
        public int AccountId { get; set; }

        public LogLevel LogLevel { get; set; }

        public string Message { get; set; }
    }
}
