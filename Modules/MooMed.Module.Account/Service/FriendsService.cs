using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
		private readonly IFriendsMappingRepository _friendsMappingRepository;

		[NotNull]
		private readonly IModelConverter<Friend, AccountEntity, int> _accountFriendConverter;

		public FriendsService(
			[NotNull] IFriendsMappingRepository friendsMappingRepository,
			[NotNull] IModelConverter<Friend, AccountEntity, int> accountFriendConverter)
		{
			_friendsMappingRepository = friendsMappingRepository;
			_accountFriendConverter = accountFriendConverter;
		}

		public async Task<List<Friend>> GetFriends(ISessionContext sessionContext)
		{
			var friends = await _friendsMappingRepository.Read(
				x => x.Id == sessionContext.Account.Id,
				x => x.Include(y => y.Friend).ThenInclude(y => y.AccountOnlineStateEntity));

			return friends.Select(x => _accountFriendConverter.ToModel(x.Friend)).ToList();
		}

		public async Task<bool> AddFriend(ISessionContext sessionContext, int accountId)
		{
			var friends = await GetFriends(sessionContext);

			if (friends.Any(x => x.Id == accountId))
			{
				return false;
			}

			await _friendsMappingRepository.Create(new FriendsMappingEntity()
			{
				Id = sessionContext.Account.Id,
				FriendId = accountId
			});

			return true;
		}
	}
}
