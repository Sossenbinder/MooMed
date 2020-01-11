using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IProfilePictureService : IGrpcService
	{
		[OperationContract]
		[NotNull]
		Task<ServiceResponse<bool>> ProcessUploadedProfilePicture([NotNull] ISessionContext sessionContext, [NotNull] ProfilePictureData profilePictureData);

		[OperationContract]
		[NotNull]
		Task<string> GetProfilePictureForAccountById([NotNull] AccountIdQuery accountIdQuery);

		[OperationContract]
		[NotNull]
        Task<string> GetProfilePictureForAccount(ISessionContext sessionContext);
    }
}