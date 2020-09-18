using System;
using MooMed.Common.Definitions.IPC;
using MooMed.ServiceBase.Attributes;

namespace GrpcProxyGenerator.DataTypes
{
	internal class ProxyMetaData
	{
		public Type Type { get; set; }

		public string InterfaceNameShort { get; set; }

		public string ServiceNameShort { get; set; }

		public ServiceType KubernetesServiceType { get; set; }
	}
}