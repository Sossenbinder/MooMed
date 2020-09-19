using Autofac;
using MooMed.AspNetCore.Modules;
using MooMed.ChatService.Remoting;
using MooMed.DependencyInjection.Extensions;
using MooMed.FinanceService.Remoting;
using MooMed.ServiceBase.Services.Interface;
using MooMed.AccountService.Remoting;
using MooMed.AccountValidationService.Remoting;
using MooMed.ProfilePictureService.Remoting;
using MooMed.SearchService.Remoting;
using MooMed.SessionService.Remoting;

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
		}
	}
}