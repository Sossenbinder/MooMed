using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace MooMed.Azure
{
    public class TableStorageAccessor
    {
        [NotNull]
        private readonly Lazy<CloudTableClient> m_cloudTableClient;

        public TableStorageAccessor([NotNull] string connectionString)
        {
            m_cloudTableClient = new Lazy<CloudTableClient>(() =>
            {
                CloudStorageAccount.TryParse(connectionString, out var account);
                return account.CreateCloudTableClient();
            });
        }

        [ItemNotNull]
        public async Task<CloudTable> GetOrCreateCloudTable([NotNull] string tableName)
        {
            var container = m_cloudTableClient.Value.GetTableReference(tableName);

            if (!await container.ExistsAsync())
            {
                await container.CreateAsync();
            }

            return container;
        }
    }
}
