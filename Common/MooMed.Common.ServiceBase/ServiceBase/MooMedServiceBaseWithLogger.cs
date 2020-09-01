using JetBrains.Annotations;
using MooMed.Common.Definitions.Logging;

namespace MooMed.Common.ServiceBase.ServiceBase
{
	public class MooMedServiceBaseWithLogger : MooMedServiceBase
	{
		[NotNull]
		protected readonly IMooMedLogger Logger;

		public MooMedServiceBaseWithLogger([NotNull] IMooMedLogger logger)
		{
			Logger = logger;
		}
	}
}