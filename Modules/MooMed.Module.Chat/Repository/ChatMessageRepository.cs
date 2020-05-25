using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<IEnumerable<ChatMessageEntity>> GetChatMessages([NotNull] Func<ChatMessageEntity, bool> predicate, int? toSkip = null, int takeCount = 100)
		{
			await using var ctx = CreateContext();

			var query = ctx.ChatMessages.Where(predicate).Take(takeCount);

			if (toSkip.HasValue)
			{
				query = query.Skip(toSkip.Value);
			}

			return query.ToList();
		}
	}
}
