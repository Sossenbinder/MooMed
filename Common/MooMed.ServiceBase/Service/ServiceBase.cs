using System.Fabric;
using JetBrains.Annotations;
using Microsoft.ServiceFabric.Services.Runtime;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.ServiceBase.Service
{
	public class ServiceBase : StatefulService
	{
		protected readonly IMainLogger Logger;

		public ServiceBase(
			[NotNull] StatefulServiceContext context,
			[NotNull] IMainLogger logger)
			: base(context)
		{
			Logger = logger;
		}
	}
}
