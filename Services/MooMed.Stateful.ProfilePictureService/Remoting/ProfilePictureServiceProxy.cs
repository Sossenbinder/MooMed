using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;
using ProtoBuf.Grpc;

namespace MooMed.Stateful.ProfilePictureService.Remoting
{
	public class ProfilePictureServiceProxy : AbstractProxy<IProfilePictureService>, IProfilePictureService
	{
		public ProfilePictureServiceProxy(
			[NotNull] IStatefulCollectionInfoProvider statefulCollectionInfoProvider,
			[NotNull] IGrpcClientProvider grpcClientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper)
			: base(statefulCollectionInfoProvider, 
				grpcClientProvider,
				deterministicPartitionSelectorHelper,
				DeployedService.ProfilePictureService)
		{
		}

		public Task<ServiceResponse<bool>> ProcessUploadedProfilePicture([NotNull] IAsyncEnumerable<byte[]> pictureStream, CallContext callContext)
			=> Invoke(1, service => service.ProcessUploadedProfilePicture(pictureStream, callContext));

		public Task<string> GetProfilePictureForAccountById(Primitive<int> accountId)
			=> InvokeOnRandomReplica(service => service.GetProfilePictureForAccountById(accountId));

		public Task<string> GetProfilePictureForAccount(ISessionContext sessionContext)
			=> Invoke(sessionContext, service => service.GetProfilePictureForAccount(sessionContext));
	}
}
