using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Module.Accounts.Service
{
	public class FriendsService : IFriendsService
	{
		[NotNull]
		private readonly IFriendsMappingRepository m_friendsMappingRepository;

		[NotNull]
		private readonly IModelConverter<Friend, AccountEntity> m_accountFriendConverter;

		public FriendsService(
			[NotNull] IFriendsMappingRepository friendsMappingRepository,
			[NotNull] IModelConverter<Friend, AccountEntity> accountFriendConverter)
		{
			m_friendsMappingRepository = friendsMappingRepository;
			m_accountFriendConverter = accountFriendConverter;
		}

		public async Task<List<Friend>> GetFriends(ISessionContext sessionContext)
		{
			var friends = await m_friendsMappingRepository.Read(
				x => x.AccountId == sessionContext.Account.Id,
				x => x.Friend);

			return friends.Select(x => m_accountFriendConverter.ToModel(x.Friend)).ToList();
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
