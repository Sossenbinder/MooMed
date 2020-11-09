using System;
using System.Collections.Generic;
using System.Linq;
using GrpcProxyGenerator.DataTypes;
using GrpcProxyGenerator.Service.Interface;
using MooMed.Common.Definitions.IPC;
using MooMed.RemotingProxies.ProxyInvocation;
using MooMed.ServiceBase.Attributes;

namespace GrpcProxyGenerator.Service
{
	internal class TypeInfoProvider : ITypeInfoProvider
	{
		private static readonly Dictionary<ServiceType, Type> ProxyTypeDict = new Dictionary<ServiceType, Type>()
		{
			{ ServiceType.Deployment, typeof(AbstractDeploymentProxy<>) },
			{ ServiceType.StatefulSet, typeof(AbstractStatefulSetProxy<>) }
		};

		public Type GetProxyTypeForType(ServiceType serviceType)
		{
			return ProxyTypeDict[serviceType];
		}

		public ProxyMetaData GetMetaData(Type proxyInterface)
		{
			var metaData = new ProxyMetaData
			{
				Type = proxyInterface,
				InterfaceNameShort = proxyInterface.Name
			};

			metaData.ServiceNameShort = metaData.InterfaceNameShort.Substring(1);

			var attributes = proxyInterface.GetCustomAttributes(false);

			var kubernetesAttribute = attributes
				.OfType<KubernetesServiceTypeAttribute>()
				.FirstOrDefault();

			metaData.KubernetesServiceType = kubernetesAttribute?.ServiceType ?? ServiceType.Deployment;

			return metaData;
		}
	}
}