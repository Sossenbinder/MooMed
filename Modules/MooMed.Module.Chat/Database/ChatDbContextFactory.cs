using MooMed.Common.Database.Context;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Module.Chat.Database
{
	public class ChatDbContextFactory : AbstractDbContextFactory<ChatDbContext>
	{
		public ChatDbContextFactory(IConfigSettingsProvider configSettingsProvider, string key) 
			: base(configSettingsProvider, key)
		{
		}

		public override ChatDbContext CreateContext()
		{
			return new ChatDbContext(GetConnectionString());
		}
	}
}
