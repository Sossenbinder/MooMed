using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
    public interface IProfilePictureService : IGrpcService
	{
		[NotNull]
		Task<bool> ProcessUploadedProfilePicture([NotNull] ISessionContext sessionContext, [NotNull] IFormFile file);

        [NotNull]
		Task<string> GetProfilePictureForAccountById(int accountId);

        [NotNull]
        Task<string> GetProfilePictureForAccount(ISessionContext sessionContext);
    }
}