using System.Fabric;
using JetBrains.Annotations;

namespace MooMed.ServiceFabric.Helper
{
	public static class FabricClientFactory
	{
		[NotNull]
		public static FabricClient Create()
		{
			return new FabricClient();
		}
	}
}
