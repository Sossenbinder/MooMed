﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Module.Chat.DataTypes.Entity;

namespace MooMed.Module.Chat.Repository.Interface
{
	public interface IChatMessageRepository : ICrudRepository<ChatMessageEntity, Guid>
	{
		Task<IEnumerable<ChatMessageEntity>> GetChatMessages(Func<ChatMessageEntity, bool> predicate, int? toSkip = null, int takeCount = 100);
	}
}