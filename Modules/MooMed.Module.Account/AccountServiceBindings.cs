using Autofac;
using JetBrains.Annotations;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Events;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;
using MooMed.Module.Accounts.Repository.Converters;

namespace MooMed.Module.Accounts
{
    public class AccountServiceBindings : Autofac.Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AccountSignInService>()
                .As<IAccountSignInService>()
                .SingleInstance();

            builder.RegisterType<AccountSignInValidator>()
                .As<IAccountSignInValidator>()
                .SingleInstance();
            
            builder.RegisterType<AccountEventHub>()
                .As<IAccountEventHub>()
                .SingleInstance();

            builder.RegisterType<AccountDataRepository>()
                .As<AccountDataRepository>()
                .SingleInstance();

            builder.RegisterType<AccountValidationDataHelper>()
                .As<AccountValidationDataHelper>()
                .SingleInstance();

            builder.RegisterType<AccountEmailValidationHelper>()
                .As<IAccountValidationEmailHelper>()
                .SingleInstance();

            builder.RegisterType<AccountValidationTokenHelper>()
                .As<IAccountValidationTokenHelper>()
                .SingleInstance();

            builder.RegisterType<AccountDbContextFactory>()
	            .AsSelf()
	            .SingleInstance()
	            .WithParameter("key", "MooMed_Database_Account");

            builder.RegisterType<AccountDbConverter>()
	            .As<IModelConverter<Account, AccountEntity>, IBiDirectionalDbConverter<Account, AccountEntity>>()
	            .SingleInstance();

            builder.RegisterType<RegisterModelAccountDbConverter>()
	            .As<IEntityConverter<RegisterModel, AccountEntity>>()
	            .SingleInstance();
        }
    }
}
