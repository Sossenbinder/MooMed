﻿using Autofac;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.ServiceBase.Services.Interface;
using MooMed.RemotingProxies.Proxies;

namespace MooMed.AccountService.Module
{
	public class AccountServiceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<ProfilePictureServiceProxy>()
				.As<IProfilePictureService>()
				.SingleInstance();

			builder.RegisterType<SessionServiceProxy>()
				.As<ISessionService>()
				.SingleInstance();

			builder.RegisterType<AccountDbConverter>()
				.AsSelf()
				.SingleInstance();
		}
	}
}