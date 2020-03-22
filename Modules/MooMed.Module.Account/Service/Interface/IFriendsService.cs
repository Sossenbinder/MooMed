using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface IFriendsService
	{
		Task<List<Friend>> GetFriends([NotNull] ISessionContext sessionContext);

		Task<bool> AddFriend([NotNull] ISessionContext sessionContext, int accountId);
	}
}
