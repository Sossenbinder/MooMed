﻿using Autofac;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Stateful.AccountService.Remoting;
using MooMed.Stateful.AccountValidationService.Remoting;
using MooMed.Stateful.ProfilePictureService.Remoting;
using MooMed.Stateful.SearchService.Remoting;
using MooMed.Stateful.SessionService.Remoting;

namespace MooMed.Web.Modules
{
	public class WebGrpcModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<AccountServiceProxy>()
				.As<IAccountService>()
				.SingleInstance();

			builder.RegisterType<AccountValidationServiceProxy>()
				.As<IAccountValidationService>()
				.SingleInstance();

			builder.RegisterType<ProfilePictureServiceProxy>()
				.As<IProfilePictureService>()
				.SingleInstance();

			builder.RegisterType<SearchServiceProxy>()
				.As<ISearchService>()
				.SingleInstance();

			builder.RegisterType<SessionServiceProxy>()
				.As<ISessionService>()
				.SingleInstance();
		}
	}
}
