using Autofac;
using JetBrains.Annotations;
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
using MooMed.Module.Accounts.Service;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Module
{
    public class AccountModule : Autofac.Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            // Services
            builder.RegisterType<AccountSignInService>()
                .As<IAccountSignInService>()
                .SingleInstance();

            builder.RegisterType<AccountSignInValidator>()
                .As<IAccountSignInValidator>()
                .SingleInstance();
            
            builder.RegisterType<AccountEventHub>()
                .As<IAccountEventHub>()
                .SingleInstance();

            // Repositories
            builder.RegisterType<AccountDataRepository>()
                .As<IAccountDataRepository>()
                .SingleInstance();

            builder.RegisterType<FriendsMappingRepository>()
	            .As<IFriendsMappingRepository>()
	            .SingleInstance();

            builder.RegisterType<AccountValidationDataHelper>()
	            .As<IAccountValidationDataHelper>()
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

            builder.RegisterType<AccountValidationDbConverter>()
	            .As<IBiDirectionalDbConverter<AccountValidation, AccountValidationEntity>>()
	            .SingleInstance();

            builder.RegisterType<FriendsService>()
	            .As<IFriendsService>()
	            .SingleInstance();
        }
    }
}
