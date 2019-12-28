using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Module.ProfilePictures.Service.Interface
{
    public interface IProfilePictureService : IGrpcService
	{
        Task<bool> ProcessUploadedProfilePicture([NotNull] ISessionContext sessionContext, [NotNull] IFormFile file);

        Task<string> GetProfilePictureForAccountById(int accountId);

        [ItemNotNull]
        Task<string> GetProfilePictureForAccount(ISessionContext sessionContext);
    }
}