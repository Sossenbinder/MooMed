using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase.Interface;
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

		public Task<bool> ProcessUploadedProfilePicture(ISessionContext sessionContext, IFormFile formFile)
			=> Invoke(sessionContext, service => service.ProcessUploadedProfilePicture(sessionContext, formFile));

		public Task<string> GetProfilePictureForAccountById(int accountId)
			=> Invoke(service => service.GetProfilePictureForAccountById(accountId));

		public Task<string> GetProfilePictureForAccount(ISessionContext sessionContext)
			=> Invoke(sessionContext, service => service.GetProfilePictureForAccount(sessionContext));
	}
}
