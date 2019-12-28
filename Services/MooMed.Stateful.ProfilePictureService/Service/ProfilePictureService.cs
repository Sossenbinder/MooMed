using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using MooMed.Azure;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.Code.Utils;

namespace MooMed.Stateful.ProfilePictureService.Service
{
	public class ProfilePictureService : MooMedServiceBase, Common.ServiceBase.Interface.IProfilePictureService
    {
        [NotNull]
        private readonly List<string> m_possibleImageExtensions;

        [NotNull]
        private const string m_defaultProfilePicture = "/Resources/Icons/Profile/profileBasic.png";

        [NotNull]
        private readonly IConfigSettingsProvider m_settingsProvider;

        private readonly CloudStorageAccessor m_cloudStorageAccessor;

        public ProfilePictureService(
	        [NotNull] IMainLogger logger,
	        [NotNull] IConfigSettingsProvider settingsProvider)
			:base(logger)
        {
            m_settingsProvider = settingsProvider;
            m_cloudStorageAccessor = new CloudStorageAccessor(settingsProvider.ReadDecryptedValueOrFail<string>("MooMed_ProfilePictures_ConnectionString", "AccountKey"));
            m_possibleImageExtensions = new List<string> { ".png", ".jpg" };
        }

        public Task<string> GetProfilePictureForAccount([NotNull] ISessionContext sessionContext)
            => GetProfilePictureForAccountById(sessionContext.Account.Id);
		
        [ItemNotNull]
        public async Task<string> GetProfilePictureForAccountById(int accountId)
        {
            if (accountId <= 0)
            {
                return m_defaultProfilePicture;
            }

            var containerName = $"a-{accountId}";
            var blobName = "80x80picture.png";


            if (!await m_cloudStorageAccessor.DoesBlobExistOnContainer(containerName, blobName))
			{
				return m_defaultProfilePicture;
			}

            var storageAccountBasePath = m_settingsProvider.ReadValueOrFail<string>("MooMed_ProfilePictures_StorageAccountUrl");

            return $"{storageAccountBasePath}/{containerName}/80x80picture.png?refreshTimer={DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
        }
		
        /// <summary>
        /// Processes uploaded profile picture
        /// </summary>
        /// <param name="sessionContext"></param>
        /// <param name="pictureData"></param>
        /// <param name="files"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> ProcessUploadedProfilePicture(ISessionContext sessionContext, IFormFile file)
        {
            if (file.Length == 0)
            {
                return false;
            }

            await using var ms = new MemoryStream();

            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var image = !m_possibleImageExtensions.Contains(file.GetFileExtension()) ? null : ImageUtils.ConvertAndScaleRequestImage(fileBytes);

            if (image != null)
            {
	            return await SaveProfilePicture(sessionContext, image, file.GetFileExtension());
            }

            return false;
        }

        /// <summary>
        /// Stores an Image to the respective directory
        /// </summary>
        /// <param name="sessionContext">SessionContext of user</param>
        /// <param name="image">Image to store</param>
        /// <param name="fileExtension">Extension of file</param>
        /// <returns>Success / Failure</returns>
        private async Task<bool> SaveProfilePicture([NotNull] ISessionContext sessionContext, [NotNull] Image image, [NotNull] string fileExtension)
        {
            var container = await m_cloudStorageAccessor.CreateContainer(sessionContext.Account.IdAsKey(), BlobContainerPublicAccessType.Blob);

            var blob = m_cloudStorageAccessor.CreateBlockBlob(container, "80x80picture.png");

            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            await using (var ms = new MemoryStream())
            {
                image.Save(ms, fileExtension.Equals(".png") ? ImageFormat.Png : ImageFormat.Jpeg);
                var imageChopped = ms.ToArray();
                await blob.UploadFromByteArrayAsync(imageChopped, 0, imageChopped.Length);
            }

            return true;
        }
    }
}
