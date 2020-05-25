using MooMed.Common.Database.Context;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Module.Chat.Database
{
	public class ChatDbContextFactory : AbstractDbContextFactory<ChatDbContext>
	{
		public ChatDbContextFactory(IConfigSettingsProvider configSettingsProvider) 
			: base(configSettingsProvider, "MooMed_Database_Chat")
		{
		}

		public override ChatDbContext CreateContext()
		{
			return new ChatDbContext(GetConnectionString());
		}
	}
}
