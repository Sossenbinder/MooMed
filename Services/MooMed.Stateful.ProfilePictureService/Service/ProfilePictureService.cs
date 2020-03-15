using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage.Blob;
using MooMed.Azure;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Stateful.ProfilePictureService.Utils;
using ProtoBuf.Grpc;
using SixLabors.ImageSharp;

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

        public Task<ServiceResponse<string>> GetProfilePictureForAccount([NotNull] ISessionContext sessionContext)
            => GetProfilePictureForAccountById(sessionContext.Account.Id);

        [ItemNotNull]
        public async Task<ServiceResponse<string>> GetProfilePictureForAccountById(Primitive<int> accountId)
        {
            if (accountId <= 0)
            {
                return ServiceResponse<string>.Success(m_defaultProfilePicture);
            }

            var containerName = $"a-{accountId.Value}";
            var blobName = "80x80picture.png";

            if (!await m_cloudStorageAccessor.DoesBlobExistOnContainer(containerName, blobName))
            {
	            return ServiceResponse<string>.Success(m_defaultProfilePicture);
            }

            var storageAccountBasePath = m_settingsProvider.ReadValueOrFail<string>("MooMed_ProfilePictures_StorageAccountUrl");

            return ServiceResponse<string>.Success($"{storageAccountBasePath}/{containerName}/80x80picture.png?refreshTimer={DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        }

        public Task<ServiceResponse<bool>> ProcessUploadedProfilePicture(IAsyncEnumerable<byte[]> pictureStream, CallContext callContext)
        {
	        var fileExtension = callContext.RequestHeaders.Single(x => x.Key.Equals("fileextension")).Value;
	        var accountId = int.Parse(callContext.RequestHeaders.Single(x => x.Key.Equals("accountid")).Value);

	        return ProcessUploadedProfilePicture(pictureStream, fileExtension, accountId);
        }

        /// <summary>
        /// Processes uploaded profile picture
        /// </summary>
        /// <param name="pictureStream"></param>
        /// <param name="fileExtension"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private async Task<ServiceResponse<bool>> ProcessUploadedProfilePicture([NotNull] IAsyncEnumerable<byte[]> pictureStream, [NotNull] string fileExtension, int accountId)
        {
	        using var imgStream = !m_possibleImageExtensions.Contains(fileExtension) ? null : await ImageUtils.ConvertAndScaleRequestImage(pictureStream, fileExtension);

	        if (imgStream == null)
	        {
		        return ServiceResponse<bool>.Failure();
	        }

	        var result = await SaveProfilePicture(accountId, imgStream, fileExtension);
	        return ServiceResponse<bool>.Success(result);
        }

        /// <summary>
        /// Stores an Image to the respective directory
        /// </summary>
        /// <param name="accountId">Account id of user</param>
        /// <param name="image">Image to store</param>
        /// <param name="fileExtension">Extension of file</param>
        /// <returns>Success / Failure</returns>
        private async Task<bool> SaveProfilePicture(int accountId, [NotNull] Image image, [NotNull] string fileExtension)
        {
            var container = await m_cloudStorageAccessor.CreatePublicContainerIfNotExists($"a-{accountId}", BlobContainerPublicAccessType.Blob);

            var blob = m_cloudStorageAccessor.CreateBlockBlob(container, "80x80picture.png");

            await using (var uploadStream = await blob.OpenWriteAsync())
            {
	            if (fileExtension.Equals(".png"))
	            {
                    image.SaveAsPng(uploadStream);
	            }
	            else
	            {
		            image.SaveAsJpeg(uploadStream);

	            }
            }

            return true;
        }
    }
}
