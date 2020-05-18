using Autofac;
using MooMed.AspNetCore.Modules;
using MooMed.ChatService.Remoting;
using MooMed.Common.ServiceBase.Interface;
using MooMed.DependencyInjection.Extensions;
using MooMed.Stateful.AccountService.Remoting;
using MooMed.Stateful.AccountValidationService.Remoting;
using MooMed.Stateful.ProfilePictureService.Remoting;
using MooMed.Stateful.SearchService.Remoting;
using MooMed.Stateful.SessionService.Remoting;

namespace MooMed.Web.Modules
{
	public class WebGrpcModule : GrpcModule
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
		}
	}
}
