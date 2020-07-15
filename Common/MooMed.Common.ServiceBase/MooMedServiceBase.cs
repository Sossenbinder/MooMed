using JetBrains.Annotations;
using MooMed.Logging.Loggers.Interface;

namespace MooMed.Common.ServiceBase
{
	public class MooMedServiceBase
	{
		[NotNull]
		protected readonly IMooMedLogger Logger;

		public MooMedServiceBase([NotNull] IMooMedLogger logger)
		{
			Logger = logger;
		}
	}
}
