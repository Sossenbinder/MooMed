using Autofac;
using MooMed.Module.Finance.Modules;

namespace MooMed.FinanceService.Module
{
	public class FinanceServiceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterModule(new InternalFinanceModule());
		}
	}
}
