using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;

namespace MooMed.Azure
{
    public class CloudStorageAccessor
    {
        private readonly Lazy<CloudBlobClient> m_cloudBlobClient;

        public CloudStorageAccessor([NotNull] string connectionString)
        {
            m_cloudBlobClient = new Lazy<CloudBlobClient>(() =>
            {
                CloudStorageAccount.TryParse(connectionString, out var account);
                return account.CreateCloudBlobClient();
            });
        }

        private CloudBlobContainer GetContainer([NotNull] string containerName)
        {
            return m_cloudBlobClient.Value.GetContainerReference(containerName);
        }

        public async Task<bool> DoesBlobExistOnContainer([NotNull] string containerName, [NotNull] string blobName)
            => await DoesBlobExistOnContainer(GetContainer(containerName), blobName);

        private async Task<bool> DoesBlobExistOnContainer([NotNull] CloudBlobContainer container, [NotNull] string blobName)
        {
	        try
	        {
		        return await container.GetBlockBlobReference(blobName).ExistsAsync();
	        }
	        catch (Exception)
	        {
		        return false;
	        }
        }

        [ItemNotNull]
        public async Task<CloudBlobContainer> CreateContainer([NotNull] string containerName, BlobContainerPublicAccessType blobContainerAccessType)
        {
            var container = GetContainer(containerName);

            if (!await container.ExistsAsync())
            {
                await container.CreateAsync();

                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = blobContainerAccessType
                });
            }

            return container;
        }

        public CloudBlockBlob CreateBlockBlob([NotNull] CloudBlobContainer blobContainer, [NotNull] string blobName)
        {
            return blobContainer.GetBlockBlobReference(blobName);
        }
    }
}
