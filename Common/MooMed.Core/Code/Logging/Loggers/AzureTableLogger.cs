using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage.Table;
using MooMed.Azure;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper;

namespace MooMed.Core.Code.Logging.Loggers
{
    public class AzureTableLogger
    {
        [NotNull]
        private readonly AsyncLazy<CloudTable> m_cloudTable;

        public AzureTableLogger(
            [NotNull] IConfigSettingsProvider settingsProvider)
        {
            var tableStorageAccessor = new TableStorageAccessor(settingsProvider.ReadDecryptedValueOrFail<string>("MooMed_Logging_TableStorageConnectionString", "AccountKey"));

            m_cloudTable = new AsyncLazy<CloudTable>(async () => await tableStorageAccessor.GetOrCreateCloudTable("Logging"));
        }

        public async Task Log([NotNull] ISessionContext sessionContext, string message, LogLevel logLevel)
        {
            var cloudTable = await m_cloudTable.Value;

            var loggingEntity = new LoggingEntity()
            {
                AccountId = sessionContext.Account.Id,
                LogLevel = logLevel,
                Message = message,
                Timestamp = DateTimeOffset.Now,
                PartitionKey = logLevel.ToString(),
                RowKey = sessionContext.Account.Id.ToString()
            };

            var insertOperation = TableOperation.Insert(loggingEntity);

            await cloudTable.ExecuteAsync(insertOperation);
        }
    }
}
