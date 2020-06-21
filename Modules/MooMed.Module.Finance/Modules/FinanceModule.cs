using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Common.Definitions.UiModels.Finance;
using MooMed.Module.Finance.Converters;

namespace MooMed.Module.Finance.Modules
{
	public class FinanceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<ExchangeTradedModelToUiModelConverter>()
				.As<IModelToUiModelConverter<ExchangeTradedModel, ExchangeTradedUiModel>>()
				.SingleInstance();

			builder.RegisterType<GetExchangeTradedsUiModelToModelConverter>()
				.As<IUiModelToModelConverter<GetExchangeTradedsUiModel, GetExchangeTradedsModel>>()
				.SingleInstance();
		}
	}
}
