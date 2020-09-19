using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;
using ProtoBuf.Grpc;

namespace MooMed.ProfilePictureService.Remoting
{
	public class ProfilePictureServiceProxy : AbstractDeploymentProxy<IProfilePictureService>, IProfilePictureService
	{
		public ProfilePictureServiceProxy([NotNull] IGrpcClientProvider grpcClientProvider)
			: base(
				grpcClientProvider,
				DeploymentService.ProfilePictureService)
		{
		}

		public Task<ServiceResponse<bool>> ProcessUploadedProfilePicture(IAsyncEnumerable<byte[]> pictureStream, CallContext callContext)
			=> InvokeWithResult(service => service.ProcessUploadedProfilePicture(pictureStream, callContext));

		public Task<ServiceResponse<string>> GetProfilePictureForAccountById(Primitive<int> accountId)
			=> InvokeWithResult(service => service.GetProfilePictureForAccountById(accountId));

		public Task<ServiceResponse<string>> GetProfilePictureForAccount(ISessionContext sessionContext)
			=> InvokeWithResult(service => service.GetProfilePictureForAccount(sessionContext));
	}
}