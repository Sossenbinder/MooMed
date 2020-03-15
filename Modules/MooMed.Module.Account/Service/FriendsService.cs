using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	public class FriendsService : IFriendsService
	{
		[NotNull]
		private readonly IFriendsMappingRepository m_friendsMappingRepository;

		public FriendsService(
			[NotNull] IFriendsMappingRepository friendsMappingRepository)
		{
			m_friendsMappingRepository = friendsMappingRepository;
		}

		public async Task<bool> AddFriend(ISessionContext sessionContext, int accountId)
		{
			await m_friendsMappingRepository.Create(new FriendsMappingEntity()
			{
				AccountId = sessionContext.Account.Id,
				FriendId = accountId
			});

			return true;
		}
	}
}
