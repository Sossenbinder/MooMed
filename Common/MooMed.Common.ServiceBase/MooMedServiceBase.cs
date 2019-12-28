using JetBrains.Annotations;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Common.ServiceBase
{
	public class MooMedServiceBase
	{
		[NotNull]
		protected readonly IMainLogger Logger;

		public MooMedServiceBase([NotNull] IMainLogger logger)
		{
			Logger = logger;
		}
	}
}
