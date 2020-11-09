using Autofac;
using MooMed.AspNetCore.Modules;
using MooMed.DependencyInjection.Extensions;
using MooMed.RemotingProxies.Proxies;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Modules
{
	public class FrontendMooMedAspNetCoreModule : MooMedAspNetCoreModule
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterGrpcService<IChatService, ChatServiceProxy>();
			builder.RegisterGrpcService<IAccountService, AccountServiceProxy>();
			builder.RegisterGrpcService<IAccountValidationService, AccountValidationServiceProxy>();
			builder.RegisterGrpcService<IProfilePictureService, ProfilePictureServiceProxy>();
			builder.RegisterGrpcService<ISearchService, SearchServiceProxy>();
			builder.RegisterGrpcService<ISessionService, SessionServiceProxy>();
			builder.RegisterGrpcService<IFinanceService, FinanceServiceProxy>();
			builder.RegisterGrpcService<ISavingService, SavingServiceProxy>();
		}
	}
}