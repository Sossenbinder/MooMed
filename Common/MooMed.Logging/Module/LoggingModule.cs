using Autofac;
using MooMed.Logging.Loggers;
using MooMed.Logging.Loggers.Helper;
using MooMed.Logging.Loggers.Interface;
using MooMed.Logging.LogManagement;
using MooMed.Logging.LogManagement.Interface;

namespace MooMed.Logging.Module
{
	public class LoggingModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<MooMedLogger>()
				.As<IMooMedLogger>()
				.SingleInstance();

			builder.RegisterType<SerilogConfigProvider>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<StaticLogger>()
				.AsSelf()
				.AutoActivate()
				.SingleInstance();

			builder
#if DEBUG
				.RegisterType<DebugLogFileManager>()
#else
				.RegisterType<ReleaseLogFileManager>()
#endif
				.As<ILogFileManager>()
				.SingleInstance();
		}
	}
}
