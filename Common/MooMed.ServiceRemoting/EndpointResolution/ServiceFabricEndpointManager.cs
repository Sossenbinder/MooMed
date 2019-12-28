using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.ServiceFabric.Helper;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.EndpointResolution.Interface;
using MooMed.ServiceRemoting.Helper;

namespace MooMed.ServiceRemoting.EndpointResolution
{
	public class ServiceFabricEndpointManager : IServiceFabricEndpointManager
	{
		[NotNull]
		private readonly FabricClient m_fabricClient;

		private readonly Dictionary<DeployedFabricApplication, Dictionary<DeployedFabricService, Service>> m_fabricServiceCache;

		public ServiceFabricEndpointManager()
		{
			m_fabricClient = FabricClientFactory.Create();

			m_fabricServiceCache = new Dictionary<DeployedFabricApplication, Dictionary<DeployedFabricService, Service>>();
		}

		public async Task<Service> GetServiceOnApp(DeployedFabricService deployedFabricService, DeployedFabricApplication deployedFabricApplication = DeployedFabricApplication.MooMed)
		{
			// Move this to event
			if (!m_fabricServiceCache.TryGetValue(deployedFabricApplication, out var serviceDictionary))
			{
				serviceDictionary = new Dictionary<DeployedFabricService, Service>();
				m_fabricServiceCache[deployedFabricApplication] = serviceDictionary;
			}

			if (!serviceDictionary.TryGetValue(deployedFabricService, out var fabricServiceUri))
			{
				var resolvedApplicationName = DeployedFabricApplicationHelper.FabricApplicationToApplicationName(deployedFabricApplication);

				var deployedApp = await GetAndCheckApplicationForValidDeployment(resolvedApplicationName);
				if (deployedApp == null)
				{
					throw new ArgumentException($"No deployed application found with the name {resolvedApplicationName}");
				}

				var resolvedServiceTypeName = DeployedFabricServiceHelper.ServiceNameToFabricService(deployedFabricService);

				var service = await GetAndCheckServiceForExistenceOnApplication(resolvedServiceTypeName, deployedApp);

				fabricServiceUri = service ?? throw new ArgumentException($"No service found with the name {resolvedServiceTypeName}");
				m_fabricServiceCache[deployedFabricApplication][deployedFabricService] = fabricServiceUri;
			}

			return fabricServiceUri;
		}

		/// <summary>
		/// Checks whether a given application exists in deployment and returns it
		/// </summary>
		/// <param name="applicationName"></param>
		/// <returns></returns>
		[ItemCanBeNull]
		private async Task<DeployedApplication> GetAndCheckApplicationForValidDeployment([NotNull] string applicationName)
		{
			var queryManager = m_fabricClient.QueryManager;

			var nodes = await queryManager.GetNodeListAsync();

			// Get deployed apps on each node
			var deployedAppListPerNodeTasks = nodes.Select(node => queryManager.GetDeployedApplicationListAsync(node.NodeName)).ToList();

			await Task.WhenAll(deployedAppListPerNodeTasks);

			foreach (var deployedAppListOnNode in deployedAppListPerNodeTasks)
			{
				// Deployed app list on a node
				var deployedAppList = deployedAppListOnNode.Result;

				// If any given node has the requested app deployed, return the app
				return deployedAppList.SingleOrDefault(app => app.ApplicationTypeName.Equals(applicationName));
			}

			return null;
		}

		[ItemCanBeNull]
		private async Task<Service> GetAndCheckServiceForExistenceOnApplication([NotNull] string serviceTypeName, [NotNull] DeployedApplication deployedApp)
		{
			var servicesOnGivenApplication = await m_fabricClient.QueryManager.GetServiceListAsync(deployedApp.ApplicationName);

			return servicesOnGivenApplication.SingleOrDefault(service => service.ServiceTypeName.Equals(serviceTypeName));
		}
	}
}
