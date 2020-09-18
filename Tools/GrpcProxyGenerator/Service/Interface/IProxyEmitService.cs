using GrpcProxyGenerator.DataTypes;

namespace GrpcProxyGenerator.Service.Interface
{
	internal interface IProxyEmitService
	{
		void EmitProxy(string builtProxy, ProxyMetaData metaData);
	}
}