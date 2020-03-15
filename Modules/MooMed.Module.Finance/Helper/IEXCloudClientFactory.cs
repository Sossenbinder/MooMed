using MooMed.Core.Code.Helper.Patterns.Factory;
using VSLee.IEXSharp;

namespace MooMed.Module.Finance.Helper
{
	public class IexCloudClientFactory : AbstractSimpleLazyFactory<IEXCloudClient>
	{
		public IexCloudClientFactory() 
			: base(CreateClient)
		{
		}

		private static IEXCloudClient CreateClient()
		{
			var client = new IEXCloudClient("pk_a5452f72e7c7414fae34c15b6ef71331", "sk_7084be3f23634d0998db421ddf795203", false, false);
			return client;
		}
	}
}
