using System.Text;
using Autofac;
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
using MooMed.Module.AccountValidation.Service;
using MooMed.Module.AccountValidation.Service.Interface;
using AccountValidationTokenHelper = MooMed.Module.Accounts.Helper.AccountValidationTokenHelper;
using IAccountValidationTokenHelper = MooMed.Module.Accounts.Helper.Interface.IAccountValidationTokenHelper;

namespace MooMed.Module.AccountValidation.Module
{
	public class InternalAccountValidationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<EmailValidationService>()
				.As<IEmailValidationService, IStartable>()
				.SingleInstance();

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

			builder.RegisterType<AccountDbConverter>()
				.As<AccountDbConverter, IEntityToModelConverter<AccountEntity, Account, int>, IBiDirectionalDbConverter<Account, AccountEntity, int>>()
				.SingleInstance();

			builder.RegisterType<AccountEventHub>()
				.As<IAccountEventHub>()
				.SingleInstance();
		}
	}
}