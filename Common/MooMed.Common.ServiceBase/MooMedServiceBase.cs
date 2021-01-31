using JetBrains.Annotations;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.Common.ServiceBase
{
	public abstract class MooMedServiceBase : MooMedServiceBaseWithoutLogger
	{
		[NotNull]
		protected readonly IMooMedLogger Logger;

		protected MooMedServiceBase([NotNull] IMooMedLogger logger)
		{
			Logger = logger;
		}
	}
}