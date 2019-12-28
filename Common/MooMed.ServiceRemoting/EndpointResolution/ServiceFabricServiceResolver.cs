using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.EndpointResolution.Interface;

namespace MooMed.ServiceRemoting.EndpointResolution
{
	public class ServiceFabricServiceResolver : IServiceFabricServiceResolver
	{
		[NotNull]
		private readonly IServiceFabricEndpointManager m_serviceFabricEndpointManager;

		public ServiceFabricServiceResolver([NotNull] IServiceFabricEndpointManager serviceFabricEndpointManager)
		{
			m_serviceFabricEndpointManager = serviceFabricEndpointManager;
		}

		public async Task<Uri> ResolveServiceToUri(DeployedFabricService deployedFabricService, DeployedFabricApplication deployedFabricApplication)
		{
			return (await m_serviceFabricEndpointManager.GetServiceOnApp(deployedFabricService, deployedFabricApplication)).ServiceName;
		}
	}
}
