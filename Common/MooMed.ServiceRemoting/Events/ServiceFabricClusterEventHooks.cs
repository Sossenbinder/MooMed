using System.Fabric;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.ServiceFabric.Helper;
using MooMed.ServiceRemoting.Events.Interface;

namespace MooMed.ServiceRemoting.Events
{
	public class ServiceFabricClusterEventHooks : IServiceFabricClusterEventHooks
	{
		[NotNull]
		private readonly FabricClient m_fabricClient;

		public ServiceFabricClusterEventHooks()
		{
			m_fabricClient = FabricClientFactory.Create();

			//InitClusterEventHooks().Wait();
		}

		private async Task InitClusterEventHooks()
		{
			//try
			//{
			//	var filterDescription = new ServiceNotificationFilterDescription
			//	{
			//		Name = new Uri("fabric:"),
			//		MatchNamePrefix = true
			//	};

			//	m_fabricClient.ServiceManager += ServiceManager_ServiceNotificationFilterMatched;
			//	filterId = await m_fabricClient.ServiceManager.RegisterServiceNotificationFilterAsync(filterDescription);


			//	long iterations = 0;

			//	while (true)
			//	{
			//		cancellationToken.ThrowIfCancellationRequested();

			//		ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

			//		await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
			//	}
			//}
		}
	}
}
