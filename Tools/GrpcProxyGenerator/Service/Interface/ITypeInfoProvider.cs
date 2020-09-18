using System;
using GrpcProxyGenerator.DataTypes;
using MooMed.Common.Definitions.IPC;

namespace GrpcProxyGenerator.Service.Interface
{
	internal interface ITypeInfoProvider
	{
		Type GetProxyTypeForType(ServiceType serviceType);

		ProxyMetaData GetMetaData(Type proxyInterface);
	}
}