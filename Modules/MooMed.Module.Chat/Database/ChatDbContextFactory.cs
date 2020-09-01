using MooMed.Common.Database.Context;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Module.Chat.Database
{
	public class ChatDbContextFactory : AbstractDbContextFactory<ChatDbContext>
	{
		public ChatDbContextFactory(IConfigProvider configProvider)
			: base(configProvider, "MooMed_Database_Chat")
		{
		}

		public override ChatDbContext CreateContext()
		{
			return new ChatDbContext(GetConnectionString());
		}
	}
}