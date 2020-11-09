using Autofac;
using MooMed.Common.Database.Context.Interface;
using MooMed.Module.Saving.Database;
using MooMed.Module.Saving.Repository;
using MooMed.Module.Saving.Repository.Interface;
using MooMed.Module.Saving.Service;
using MooMed.Module.Saving.Service.Interface;

namespace MooMed.Module.Saving.Modules
{
	public class InternalSavingModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SavingDbContextFactory>()
				.As<IDbContextFactory<SavingDbContext>>()
				.SingleInstance();

			builder.RegisterType<CurrencyMappingRepository>()
				.As<ICurrencyMappingRepository>()
				.SingleInstance();

			builder.RegisterType<CurrencyService>()
				.As<ICurrencyService>()
				.SingleInstance();
		}
	}
}