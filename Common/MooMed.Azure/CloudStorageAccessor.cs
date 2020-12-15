using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace MooMed.Azure
{
    public class CloudStorageAccessor
    {
        private readonly Lazy<CloudBlobClient> _cloudBlobClient;

        public CloudStorageAccessor(string connectionString)
        {
            _cloudBlobClient = new Lazy<CloudBlobClient>(() =>
            {
                var _ = CloudStorageAccount.TryParse(connectionString, out var account);
                return account.CreateCloudBlobClient();
            });
        }

        private CloudBlobContainer GetContainer(string containerName)
        {
            return _cloudBlobClient.Value.GetContainerReference(containerName);
        }

        public async Task<bool> DoesBlobExistOnContainer(string containerName, string blobName)
            => await DoesBlobExistOnContainer(GetContainer(containerName), blobName);

        private static async Task<bool> DoesBlobExistOnContainer(CloudBlobContainer container, string blobName)
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

        public async Task<CloudBlobContainer> CreatePublicContainerIfNotExists(string containerName, BlobContainerPublicAccessType blobContainerAccessType)
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

        public CloudBlockBlob CreateBlockBlob(CloudBlobContainer blobContainer, string blobName)
        {
            return blobContainer.GetBlockBlobReference(blobName);
        }
    }
}