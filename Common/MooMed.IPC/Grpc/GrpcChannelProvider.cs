using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.DataType;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.Helper;
using ProtoBuf.Grpc.Client;

namespace MooMed.IPC.Grpc
{
	/// <summary>
	/// Provides Grpc Channels for the caller
	/// </summary>
	public class GrpcChannelProvider : IGrpcChannelProvider
	{
		private const int PORT = 10042;

		[NotNull]
		private readonly IEndpointProvider _endpointProvider;

		[NotNull]
		private readonly ConcurrentDictionary<MooMedService, ConcurrentDictionary<int, GrpcChannel>> _grpcChannels;

		public GrpcChannelProvider([NotNull] IEndpointProvider endpointProvider)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			_endpointProvider = endpointProvider;

			_grpcChannels = new ConcurrentDictionary<MooMedService, ConcurrentDictionary<int, GrpcChannel>>();
		}

		public GrpcChannel GetGrpcChannel(MooMedService moomedService, int replicaNumber = 0)
		{
			if (_grpcChannels.TryGetValue(moomedService, out var serviceChannelDict))
			{
				if (serviceChannelDict.ContainsKey(replicaNumber))
				{
					return serviceChannelDict[replicaNumber];
				}
			}

			RefreshServiceChannels(moomedService);

			return _grpcChannels[moomedService][replicaNumber]; 
		}

		private void RefreshServiceChannels(MooMedService moomedService)
		{
			_grpcChannels.AddOrUpdate(
				moomedService,
				RefreshServices,
				(key, _) => RefreshServices(key));
		}

		private ConcurrentDictionary<int, GrpcChannel> RefreshServices(MooMedService moomedService)
		{
			var channelDict = new ConcurrentDictionary<int, GrpcChannel>();

			var serviceType = ServiceTypeResolver.GetServiceTypeForService(moomedService);

			switch (serviceType)
			{
				case ServiceType.Deployment:
				{
					var deploymentService = ServiceTypeResolver.TranslateMooMedServiceToDeploymentService(moomedService);

					var deploymentEndpoint = _endpointProvider.GetDeploymentEndpoint(deploymentService);

					channelDict.TryAdd(0, GrpcChannel.ForAddress($"http://{deploymentEndpoint.IpAddress}:{PORT}"));
					break;
				}
				case ServiceType.StatefulSet:
				{
					var statefulService = ServiceTypeResolver.TranslateMooMedServiceToStatefulSetService(moomedService);

					var statefulCollection = _endpointProvider.GetStatefulSetEndpointCollectionInfoForService(statefulService);

					foreach (var statefulCollectionEndpoint in statefulCollection.Endpoints)
					{
						channelDict.TryAdd(statefulCollectionEndpoint.InstanceNumber, GrpcChannel.ForAddress($"http://{statefulCollectionEndpoint.IpAddress}:{PORT}"));
					}

					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
			}

			return channelDict;
		}
	}
}
