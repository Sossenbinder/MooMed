using System;
using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Database.Repository;
using MooMed.Module.Chat.Database;
using MooMed.Module.Chat.DataTypes.Entity;
using MooMed.Module.Chat.Repository.Interface;

namespace MooMed.Module.Chat.Repository
{
	public class ChatMessageRepository : AbstractCrudRepository<ChatDbContext, ChatMessageEntity, Guid>, IChatMessageRepository
	{
		public ChatMessageRepository([NotNull] AbstractDbContextFactory<ChatDbContext> contextFactory) 
			: base(contextFactory)
		{
		}
	}
}
