using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.IPC.ProxyInvocation.Interface;

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

		public Task<ServiceResponse<bool>> ProcessUploadedProfilePicture(ISessionContext sessionContext, ProfilePictureData profilePictureData)
			=> Invoke(sessionContext, service => service.ProcessUploadedProfilePicture(sessionContext, profilePictureData));

		public Task<string> GetProfilePictureForAccountById(AccountIdQuery accountIdQuery)
			=> Invoke(service => service.GetProfilePictureForAccountById(accountIdQuery));

		public Task<string> GetProfilePictureForAccount(ISessionContext sessionContext)
			=> Invoke(sessionContext, service => service.GetProfilePictureForAccount(sessionContext));
	}
}
