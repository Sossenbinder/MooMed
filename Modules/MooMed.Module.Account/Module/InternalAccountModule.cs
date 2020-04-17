﻿using Autofac;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Helper;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Module
{
	public class InternalAccountModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterModule<AccountModule>();

			// Services
			builder.RegisterType<AccountSignInService>()
				.As<IAccountSignInService>()
				.SingleInstance();

			builder.RegisterType<AccountOnlineStateService>()
				.As<IAccountOnlineStateService>()
				.SingleInstance()
				.AutoActivate();

			builder.RegisterType<AccountSignInValidator>()
				.As<IAccountSignInValidator>()
				.SingleInstance();


			// Repositories

			builder.RegisterType<AccountDbContextFactory>()
				.AsSelf()
				.SingleInstance()
				.WithParameter("key", "MooMed_Database_Account");

			builder.RegisterType<AccountRepository>()
				.As<IAccountRepository>()
				.SingleInstance();

			builder.RegisterType<FriendsMappingRepository>()
				.As<IFriendsMappingRepository>()
				.SingleInstance();

			builder.RegisterType<AccountValidationRepository>()
				.As<IAccountValidationRepository, AccountValidationRepository>()
				.SingleInstance();

			builder.RegisterType<AccountOnlineStateRepository>()
				.As<IAccountOnlineStateRepository>()
				.SingleInstance();

			// Converters

			builder.RegisterType<AccountEmailValidationHelper>()
				.As<IAccountValidationEmailHelper>()
				.SingleInstance();

			builder.RegisterType<AccountValidationTokenHelper>()
				.As<IAccountValidationTokenHelper>()
				.SingleInstance();

			builder.RegisterType<AccountDbConverter>()
				.As<IModelConverter<Account, AccountEntity, int>, IBiDirectionalDbConverter<Account, AccountEntity, int>>()
				.SingleInstance();

			builder.RegisterType<FriendsMappingDbConverter>()
				.As<IModelConverter<Friend, AccountEntity, int>>()
				.SingleInstance();

			builder.RegisterType<RegisterModelAccountDbConverter>()
				.As<IEntityConverter<RegisterModel, AccountEntity, int>>()
				.SingleInstance();

			builder.RegisterType<AccountValidationDbConverter>()
				.As<IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity, int>>()
				.SingleInstance();

			builder.RegisterType<FriendsService>()
				.As<IFriendsService>()
				.SingleInstance();
		}
	}
}