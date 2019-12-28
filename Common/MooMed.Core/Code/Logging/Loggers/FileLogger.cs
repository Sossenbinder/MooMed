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
        private readonly string m_logFilePath;

        public FileLogger(
            [NotNull] ILogPathProvider logPathProvider)
        {
            m_logFilePath = logPathProvider.GetMainLogPath();
        }

        public async Task Log([NotNull] ISessionContext sessionContext, string message, LogLevel logLevel)
        {
            var stringMetaData = FormMetadataStringPart(logLevel, sessionContext);

            await FileExtensions.AppendAllTextAsync(m_logFilePath, $"{stringMetaData} --- {message}" + Environment.NewLine);
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
