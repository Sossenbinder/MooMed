using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace MooMed.Azure
{
    public class TableStorageAccessor
    {
        private readonly Lazy<CloudTableClient> _cloudTableClient;

        public TableStorageAccessor(string connectionString)
        {
            _cloudTableClient = new Lazy<CloudTableClient>(() =>
            {
                CloudStorageAccount.TryParse(connectionString, out var account);
                return account.CreateCloudTableClient();
            });
        }

        public async Task<CloudTable> GetOrCreateCloudTable(string tableName)
        {
            var container = _cloudTableClient.Value.GetTableReference(tableName);

            if (!await container.ExistsAsync())
            {
                await container.CreateAsync();
            }

            return container;
        }
    }
}