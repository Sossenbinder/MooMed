using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Chat.Service.Interface
{
	public interface IMessageService
	{
		public Task<IEnumerable<ChatMessageModel>> GetMessages([NotNull] ISessionContext sessionContext, int receiverId, int? continuationToken = null);

		public Task StoreMessage([NotNull] ISessionContext sessionContext, [NotNull] ChatMessageModel messageModel);
	}
}
