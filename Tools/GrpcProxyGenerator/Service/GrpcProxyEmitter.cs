using GrpcProxyGenerator.DataTypes;
using GrpcProxyGenerator.Service.Interface;

namespace GrpcProxyGenerator.Service
{
	internal class GrpcProxyEmitter : IGrpcProxyEmitter
	{
		private readonly string _solutionPath;

		public GrpcProxyEmitter(string solutionPath)
		{
			_solutionPath = solutionPath;
		}

		public void EmitProxy(string builtProxy, ProxyMetaData metaData)
		{
		}
	}
}