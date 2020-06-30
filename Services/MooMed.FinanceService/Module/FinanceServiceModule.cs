using Autofac;
using MooMed.Module.Finance.Modules;
using MooMed.Module.Portfolio.Module;

namespace MooMed.FinanceService.Module
{
	public class FinanceServiceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterModule(new InternalFinanceModule());
			builder.RegisterModule<PortfolioModule>();
		}
	}
}
