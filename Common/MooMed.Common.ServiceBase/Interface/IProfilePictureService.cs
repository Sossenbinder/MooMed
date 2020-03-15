using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;
using ProtoBuf.Grpc;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IProfilePictureService : IGrpcService
	{
		[OperationContract]
		[NotNull]
		Task<ServiceResponse<bool>> ProcessUploadedProfilePicture([NotNull] IAsyncEnumerable<byte[]> pictureStream, CallContext callContext);

		[OperationContract]
		[NotNull]
		Task<ServiceResponse<string>> GetProfilePictureForAccountById([NotNull] Primitive<int> accountId);

		[OperationContract]
		[NotNull]
        Task<ServiceResponse<string>> GetProfilePictureForAccount(ISessionContext sessionContext);
    }
}