using MooMed.Common.Database.Context;

namespace MooMed.Module.Chat.Database
{
	public class ChatDbContext : AbstractDbContext
	{
		public ChatDbContext(string connectionString) 
			: base(connectionString)
		{
		}
	}
}
