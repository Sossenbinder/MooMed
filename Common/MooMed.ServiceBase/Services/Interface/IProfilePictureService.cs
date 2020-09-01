using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;
using ProtoBuf.Grpc;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface IProfilePictureService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse<bool>> ProcessUploadedProfilePicture([NotNull] IAsyncEnumerable<byte[]> pictureStream, CallContext callContext);

		[OperationContract]
		Task<ServiceResponse<string>> GetProfilePictureForAccountById([NotNull] Primitive<int> accountId);

		[OperationContract]
		Task<ServiceResponse<string>> GetProfilePictureForAccount(ISessionContext sessionContext);
	}
}