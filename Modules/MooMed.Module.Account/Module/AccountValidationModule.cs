using System.Text;
using Autofac;
using Microsoft.AspNetCore.Identity;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Events;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.Accounts.Repository.Interface;

namespace MooMed.Module.Accounts.Module
{
	public class AccountValidationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<AccountDbContextFactory>()
				.AsSelf()
				.SingleInstance()
				.WithParameter("key", "MooMed_Database_Account");

			builder.RegisterType<AccountValidationRepository>()
				.As<IAccountValidationRepository, AccountValidationRepository>()
				.SingleInstance();

			builder.RegisterType<AccountEmailValidationHelper>()
				.As<IAccountValidationEmailHelper>()
				.SingleInstance();

			builder.RegisterInstance(Encoding.UTF8)
				.As<Encoding>()
				.SingleInstance();

			builder.RegisterType<AccountValidationTokenHelper>()
				.As<IAccountValidationTokenHelper>()
				.SingleInstance();

			builder.RegisterType<AccountValidationDbConverter>()
				.As<IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity, int>>()
				.SingleInstance();
		}
	}
}