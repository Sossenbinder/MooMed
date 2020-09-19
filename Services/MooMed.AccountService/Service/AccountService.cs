using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Logging;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.Core.DataTypes;
using MooMed.DotNet.Extensions;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Service.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.AccountService.Service
{
	public class AccountService : MooMedServiceBaseWithLogger, IAccountService
	{
		[NotNull]
		private readonly IAccountEventHub _accountEventHub;

		[NotNull]
		private readonly IRegisterService _registerService;

		[NotNull]
		private readonly ILoginService _loginService;

		[NotNull]
		private readonly IProfilePictureService _profilePictureService;

		[NotNull]
		private readonly ISessionService _sessionService;

		[NotNull]
		private readonly IFriendsService _friendsService;

		[NotNull]
		private readonly IUserService _userService;

		[NotNull]
		private readonly IPersonalDataService _personalDataService;

		public AccountService(
			[NotNull] IMooMedLogger logger,
			[NotNull] IRegisterService registerService,
			[NotNull] ILoginService loginService,
			[NotNull] IAccountEventHub accountEventHub,
			[NotNull] IProfilePictureService profilePictureService,
			[NotNull] ISessionService sessionService,
			[NotNull] IFriendsService friendsService,
			[NotNull] IUserService userService,
			[NotNull] IPersonalDataService personalDataService)
			: base(logger)
		{
			_registerService = registerService;
			_loginService = loginService;
			_accountEventHub = accountEventHub;
			_profilePictureService = profilePictureService;
			_sessionService = sessionService;
			_friendsService = friendsService;
			_userService = userService;
			_personalDataService = personalDataService;
		}

		/// <summary>
		/// Login an account
		/// </summary>
		/// <param name="loginModel">Login model of that account</param>
		/// <returns></returns>
		public async Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
		{
			var loginResult = await _loginService.Login(loginModel);

			if (loginResult.IdentityErrorCode != IdentityErrorCode.Success)
			{
				return ServiceResponse.Failure(loginResult);
			}

			// Get the profile picture for the account
			var profilePictureResponse = await _profilePictureService.GetProfilePictureForAccountById(loginResult.Account.Id);
			loginResult.Account.ProfilePicturePath = profilePictureResponse.PayloadOrNull;

			var sessionContext = await _sessionService.LoginAccount(loginResult.Account);

			await _accountEventHub.AccountLoggedIn.Raise(new AccountLoggedInEvent(sessionContext));

			return ServiceResponse.Success(loginResult);
		}

		/// <summary>
		/// Refresh login for an account which is already authenticated but lost its session
		/// </summary>
		/// <param name="accountId">Account id of account to re-login</param>
		/// <returns></returns>
		public async Task RefreshLoginForAccount(Primitive<int> accountId)
		{
			var accountResponse = await FindById(accountId);

			if (accountResponse.IsFailure)
			{
				throw new AuthenticationException("Account logged in but could not be found");
			}

			var account = accountResponse.PayloadOrFail;

			var profilePictureResponse = await _profilePictureService.GetProfilePictureForAccountById(accountId);

			account.ProfilePicturePath = profilePictureResponse.PayloadOrNull;

			var sessionContext = await _sessionService.LoginAccount(account);

			await _loginService.RefreshLastAccessed(sessionContext);

			await _accountEventHub.AccountLoggedIn.Raise(new AccountLoggedInEvent(sessionContext));
		}

		/// <summary>
		/// Registers an account and logs it in if the register was successful
		/// </summary>
		/// <param name="registerModel">Register model of that account</param>
		/// <returns></returns>
		public async Task<ServiceResponse<RegistrationResult>> Register(RegisterModel registerModel)
		{
			var registrationResult = await _registerService.Register(registerModel);
			return ServiceResponse.Success(registrationResult);
		}

		public async Task<ServiceResponse> LogOff(ISessionContext sessionContext)
		{
			Logger.Info($"Logging {sessionContext.Account.Id} off.");
			await _accountEventHub.AccountLoggedOut.Raise(new AccountLoggedOutEvent(sessionContext));
			return ServiceResponse.Success();
		}

		public async Task<ServiceResponse<Account>> FindById(Primitive<int> accountId)
		{
			var account = await _userService.FindById(accountId);
			return account == null ? ServiceResponse<Account>.Failure() : ServiceResponse<Account>.Success(account);
		}

		public async Task<ServiceResponse<List<Account>>> FindAccountsStartingWithName(string name)
		{
			var accounts = await _userService.FindAccountsStartingWithName(name);
			return ServiceResponse<List<Account>>.Success(accounts);
		}

		public async Task<ServiceResponse<Account>> FindByEmail(string email)
		{
			var account = await _userService.FindByEmail(email);
			return ServiceResponse<Account>.Success(account);
		}

		public async Task<ServiceResponse> AddAsFriend(AddAsFriendModel model)
		{
			var addResult = await _friendsService.AddFriend(model.SessionContext, model.AccountId);

			return addResult ? ServiceResponse.Success() : ServiceResponse.Failure();
		}

		public async Task<ServiceResponse<List<Friend>>> GetFriends(ISessionContext sessionContext)
		{
			var friends = await _friendsService.GetFriends(sessionContext);

			if (friends.IsNullOrEmpty())
			{
				return ServiceResponse<List<Friend>>.Success(null);
			}

			await friends.ParallelAsync(async friend =>
			{
				var profilePictureResponse = await _profilePictureService.GetProfilePictureForAccountById(friend.Id);

				if (profilePictureResponse.IsSuccess)
				{
					friend.ProfilePicturePath = profilePictureResponse.PayloadOrFail;
				}
			});

			return ServiceResponse.Success(friends);
		}

		public Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalData)
			=> _personalDataService.UpdatePersonalData(personalData);

		public Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePasswordData)
			=> _personalDataService.UpdatePassword(updatePasswordData);
	}
}