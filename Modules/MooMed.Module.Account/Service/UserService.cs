using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Repository.Converters;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Module.Accounts.Service
{
	internal class UserService : IUserService
	{
		[NotNull]
		private readonly IAccountRepository _accountRepository;

		[NotNull]
		private readonly IProfilePictureService _profilePictureService;

		[NotNull]
		private readonly AccountDbConverter _accountDbConverter;

		public UserService(
			[NotNull] IAccountRepository accountRepository,
			[NotNull] IProfilePictureService profilePictureService,
			[NotNull] AccountDbConverter accountDbConverter)
		{
			_accountRepository = accountRepository;
			_profilePictureService = profilePictureService;
			_accountDbConverter = accountDbConverter;
		}

		public async Task<Account?> FindById(int accountId)
		{
			var accountEntity = (await _accountRepository.Read(x => x.Id == accountId)).SingleOrDefault();

			if (accountEntity == null)
			{
				return null;
			}

			var account = _accountDbConverter.ToModel(accountEntity);

			account.ProfilePicturePath = (await _profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;

			return account;
		}

		[ItemNotNull]
		public async Task<List<Account>> FindAccountsStartingWithName(string name)
		{
			var accounts = (await _accountRepository.FindAccounts(acc => acc.UserName.StartsWith(name)))
				.Where(x => x != null)
				.ToList()
				.ConvertAll(accDbModel => _accountDbConverter.ToModel(accDbModel));

			foreach (var account in accounts.Where(account => account != null))
			{
				account.ProfilePicturePath = (await _profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;
			}

			return accounts;
		}

		[ItemCanBeNull]
		public async Task<Account?> FindByEmail(string email)
		{
			var accountEntity = await _accountRepository.FindAccount(acc => email.Equals(acc.Email));

			if (accountEntity == null)
			{
				return null;
			}

			var account = _accountDbConverter.ToModel(accountEntity);

			account.ProfilePicturePath = (await _profilePictureService.GetProfilePictureForAccountById(accountEntity.Id)).PayloadOrNull;

			return account;
		}
	}
}