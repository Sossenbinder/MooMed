using System;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Module.Chat.DataTypes.Entity;

namespace MooMed.Module.Chat.Repository.Interface
{
	public interface IChatMessageRepository : ICrudRepository<ChatMessageEntity, Guid>
	{
	}
}
