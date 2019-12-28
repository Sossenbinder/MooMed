using System;
using System.Fabric;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Caching.Cache.CacheImplementations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.ServiceFabric.Helper;
using MooMed.ServiceRemoting.DataType;
using MooMed.ServiceRemoting.DataType.Partition;
using MooMed.ServiceRemoting.EndpointResolution.Interface;
using MooMed.ServiceRemoting.Helper;

namespace MooMed.ServiceRemoting.EndpointResolution
{
	public class PartitionInfoProvider : IPartitionInfoProvider
	{
		[NotNull]
		private readonly IServiceFabricServiceResolver m_serviceFabricServiceResolver;

		[NotNull]
		private readonly FabricClient m_fabricClient;

		[NotNull]
		private readonly ICache<DeployedFabricService, CompositeServicePartitionInfo> m_partitionInfoCache;

		public PartitionInfoProvider([NotNull] IServiceFabricServiceResolver serviceFabricServiceResolver)
		{
			m_serviceFabricServiceResolver = serviceFabricServiceResolver;
			m_fabricClient = FabricClientFactory.Create();

			m_partitionInfoCache = new ObjectCache<DeployedFabricService, CompositeServicePartitionInfo>(CacheSettingsProvider.PartitionInfoCacheSettings);
		}

		public async Task<CompositeServicePartitionInfo> GetPartitionInfoForService(DeployedFabricService deployedFabricService)
		{
			if (m_partitionInfoCache.HasValue(deployedFabricService))
			{
				return m_partitionInfoCache.GetItem(deployedFabricService) ?? throw new InvalidOperationException();
			}

			var partitionList = await m_fabricClient.QueryManager.GetPartitionListAsync(await m_serviceFabricServiceResolver.ResolveServiceToUri(deployedFabricService, DeployedFabricApplication.MooMed));

			var compositePartition = new CompositeServicePartitionInfo(deployedFabricService);
			
			foreach (var partition in partitionList)
			{
				if (partition.PartitionInformation is Int64RangePartitionInformation int64RangePartitionInfo)
				{
					compositePartition.Partitions.Add(new PartitionInfo(int64RangePartitionInfo.LowKey, int64RangePartitionInfo.HighKey));
				}
			}

			m_partitionInfoCache.PutItem(deployedFabricService, compositePartition);

			return compositePartition;
		}
	}
}
