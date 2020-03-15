using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Friends.Service.Interface;

namespace MooMed.Module.Friends.Service
{
	public class FriendsService : IFriendsService
	{
		public Task<bool> AddFriend(ISessionContext sessionContext, int accountId)
		{
			throw new System.NotImplementedException();
		}
	}
}
