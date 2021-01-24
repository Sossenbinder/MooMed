using JetBrains.Annotations;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.Common.ServiceBase.ServiceBase
{
	public class ServiceBaseWithLogger : ServiceBase
	{
		[NotNull]
		protected readonly IMooMedLogger Logger;

		public ServiceBaseWithLogger([NotNull] IMooMedLogger logger)
		{
			Logger = logger;
		}
	}
}