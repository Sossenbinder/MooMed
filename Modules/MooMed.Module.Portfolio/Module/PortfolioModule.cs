using Autofac;
using MooMed.Common.Database.Context;
using MooMed.Module.Portfolio.Converter;
using MooMed.Module.Portfolio.Database;
using MooMed.Module.Portfolio.Repository;
using MooMed.Module.Portfolio.Repository.Interface;
using MooMed.Module.Portfolio.Service;
using MooMed.Module.Portfolio.Service.Interface;

namespace MooMed.Module.Portfolio.Module
{
	public class PortfolioModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<PortfolioService>()
				.As<IPortfolioService>()
				.SingleInstance();

			builder.RegisterType<PortfolioItemConverter>()
				.As<PortfolioItemConverter>()
				.SingleInstance();

			builder.RegisterType<PortfolioMappingRepository>()
				.As<IPortfolioMappingRepository>()
				.SingleInstance();

			builder.RegisterType<PortfolioDbContextFactory>()
				.As<PortfolioDbContextFactory, AbstractDbContextFactory<PortfolioDbContext>>()
				.SingleInstance();
		}
	}
}
