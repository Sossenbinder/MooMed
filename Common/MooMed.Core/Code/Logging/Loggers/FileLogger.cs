using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.Core.Code.Logging.LogManagement.Interface;

namespace MooMed.Core.Code.Logging.Loggers
{
    public class FileLogger
    {
        [NotNull]
        private readonly string _logFilePath;

        public FileLogger(
            [NotNull] ILogPathProvider logPathProvider)
        {
            _logFilePath = logPathProvider.GetMainLogPath();
        }

        public async Task Log([NotNull] ISessionContext sessionContext, string message, LogLevel logLevel)
        {
            var stringMetaData = FormMetadataStringPart(logLevel, sessionContext);

            await FileExtensions.AppendAllTextAsync(_logFilePath, $"{stringMetaData} --- {message}" + Environment.NewLine);
        }

        [NotNull]
        private string FormMetadataStringPart(LogLevel logLevel, [NotNull] ISessionContext sessionContext)
        {
            var datePart = DateTime.UtcNow.ToString("G");
            var logLevelPart = logLevel.ToString();
            var accountInfo = sessionContext.Account.Id;

            return $"{datePart} - {logLevelPart} - Id: {accountInfo}";
        }
    }
}
