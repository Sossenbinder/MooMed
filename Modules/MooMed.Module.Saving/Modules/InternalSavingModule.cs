using Autofac;
using Microsoft.EntityFrameworkCore;
using MooMed.Module.Saving.Converters;
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
			builder.RegisterModule<SavingModule>();

			builder.RegisterType<SavingDbContextFactory>()
				.As<IDbContextFactory<SavingDbContext>>()
				.SingleInstance();

			builder.RegisterType<CashFlowItemTypeRepository>()
				.As<ICashFlowItemRepository>()
				.SingleInstance();

			builder.RegisterType<CurrencyMappingRepository>()
				.As<ICurrencyMappingRepository>()
				.SingleInstance();

			builder.RegisterType<CurrencyService>()
				.As<ICurrencyService>()
				.SingleInstance();

			builder.RegisterType<CashFlowItemEntityConverter>()
				.As<CashFlowItemEntityConverter>()
				.SingleInstance();

			builder.RegisterType<AssetEntityConverter>()
				.As<AssetEntityConverter>()
				.SingleInstance();

			builder.RegisterType<CashFlowItemService>()
				.As<ICashFlowItemService>()
				.SingleInstance();

			builder.RegisterType<AssetRepository>()
				.As<IAssetRepository>()
				.SingleInstance();

			builder.RegisterType<AssetService>()
				.As<IAssetService>()
				.SingleInstance();

			builder.RegisterType<AssetRepository>()
				.As<IAssetRepository>()
				.SingleInstance();
		}
	}
}