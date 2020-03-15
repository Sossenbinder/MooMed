using Autofac;
using MooMed.Module.Finance.Helper;
using MooMed.Module.Finance.Helper.Interface;
using MooMed.Module.Finance.Service;
using MooMed.Module.Finance.Service.Interface;

namespace MooMed.Module.Finance.Modules
{
	public class FinanceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<IexCloudClientFactory>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<EtfDbQuerier>()
				.As<IEtfQuerier>()
				.SingleInstance();

			builder.RegisterType<EtfDataService>()
				.As<IEtfDataService, IStartable>()
				.SingleInstance();


		}
	}
}
