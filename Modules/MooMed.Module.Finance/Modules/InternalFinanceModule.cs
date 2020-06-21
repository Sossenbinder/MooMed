using Autofac;
using MooMed.Common.Database.Context;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Module.Finance.Converters;
using MooMed.Module.Finance.Database;
using MooMed.Module.Finance.Database.Entities;
using MooMed.Module.Finance.Repository;
using MooMed.Module.Finance.Repository.Interface;
using MooMed.Module.Finance.Service;
using MooMed.Module.Finance.Service.Interface;

namespace MooMed.Module.Finance.Modules
{
	public class InternalFinanceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterModule<FinanceModule>();

			builder.RegisterType<ExchangeTradedsService>()
				.As<IExchangeTradedsService>()
				.SingleInstance();

			builder.RegisterType<ExchangeTradedEntityToModelConverter>()
				.As<IEntityToModelConverter<ExchangeTradedEntity, ExchangeTradedModel, string>>()
				.SingleInstance();

			builder.RegisterType<ExchangeTradedRepository>()
				.As<IExchangeTradedRepository>()
				.SingleInstance();

			builder.RegisterType<FinanceDbContextFactory>()
				.As<FinanceDbContextFactory, AbstractDbContextFactory<FinanceDbContext>>()
				.SingleInstance();
		}
	}
}
