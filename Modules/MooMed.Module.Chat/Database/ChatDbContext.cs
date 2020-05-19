using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Module.Chat.DataTypes.Entity;

namespace MooMed.Module.Chat.Database
{
	public class ChatDbContext : AbstractDbContext
	{
		public DbSet<ChatMessageEntity> ChatMessages { get; set; }

		public ChatDbContext(string connectionString) 
			: base(connectionString)
		{
		}
	}
}
