using System.Collections.Generic;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Chat.Service.Interface
{
	public interface IMessageService
	{
		public Task<IEnumerable<ChatMessage>> GetMessages(ISessionContext sessionContext, int receiverId, int? continuationToken = null);

		public Task StoreMessage(ISessionContext sessionContext, ChatMessage message);
	}
}