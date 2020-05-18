using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Eventing.User;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Messages.Account;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.Code.Tasks;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Stateful.AccountService.Service
{
    public class AccountService : Common.ServiceBase.MooMedServiceBase, IAccountService
    {
	    [NotNull]
        private readonly IAccountEventHub _accountEventHub;

        [NotNull]
        private readonly IRegisterService _registerService;

        [NotNull]
        private readonly ILoginService _loginService;

        [NotNull]
        private readonly IAccountRepository _accountRepository;

        [NotNull]
        private readonly IProfilePictureService _profilePictureService;

        [NotNull]
        private readonly ISessionService _sessionService;

        [NotNull]
        private readonly IAccountValidationService _accountValidationService;

        [NotNull]
        private readonly IFriendsService _friendsService;

        public AccountService(
            [NotNull] IMainLogger logger,
            [NotNull] IRegisterService registerService,
            [NotNull] ILoginService loginService,
            [NotNull] IAccountEventHub accountEventHub,
            [NotNull] IAccountRepository accountRepository,
            [NotNull] IProfilePictureService profilePictureService,
            [NotNull] ISessionService sessionService,
            [NotNull] IAccountValidationService accountValidationService,
            [NotNull] IFriendsService friendsService)
            : base(logger)
        {
	        _registerService = registerService;
	        _loginService = loginService;
	        _accountEventHub = accountEventHub;
            _accountRepository = accountRepository;
            _profilePictureService = profilePictureService;
            _sessionService = sessionService;
            _accountValidationService = accountValidationService;
            _friendsService = friendsService;
        }
        /// <summary>
        /// Login an account
        /// </summary>
        /// <param name="loginModel">Login model of that account</param>
        /// <returns></returns>
        [ItemNotNull]
        public async Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
        {
            // Do the actual login in the AccountManager
            var loginResult = await _loginService.Login(loginModel);
            
            if (!loginResult.IsSuccess)
            {
                return loginResult;
            }
			
            var payload = loginResult.PayloadOrFail;
            
            var profilePictureResponse = await _profilePictureService.GetProfilePictureForAccountById(payload.Account.Id);

            payload.Account.ProfilePicturePath = profilePictureResponse.PayloadOrNull;

            var sessionContext = await _sessionService.LoginAccount(payload.Account);

            Logger.Info("Login happened", sessionContext);

            await _accountEventHub.AccountLoggedIn.Raise(new AccountLoggedInEvent(sessionContext));

            return loginResult;
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
        [ItemNotNull]
        public async Task<ServiceResponse<RegistrationResult>> Register(RegisterModel registerModel)
        {
	        var registrationResult = await _registerService.Register(registerModel);

            if (registrationResult.IsSuccess)
            {
                BackgroundTask.Run(() =>
	                _accountValidationService.SendAccountValidationMail(new AccountValidationMailData()
	                {
		                Account = registrationResult.Account,
		                Language = registerModel.Language
	                }));
            }

            return ServiceResponse<RegistrationResult>.Success(registrationResult);
        }

        public async Task<ServiceResponse> LogOff(ISessionContext sessionContext)
        {
	        Logger.Info($"Logging {sessionContext.Account.Id} off.");
            await _accountEventHub.AccountLoggedOut.Raise(new AccountLoggedOutEvent(sessionContext));

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse<Account>> FindById(Primitive<int> accountId)
        {
	        var account = (await _accountRepository.Read(acc => acc.Id == accountId, acc => acc.AccountOnlineStateEntity)).SingleOrDefault();

            var accountAsModel = account?.ToModel();

            if (accountAsModel != null)
            {
	            accountAsModel.ProfilePicturePath = (await _profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;

                return ServiceResponse<Account>.Success(accountAsModel);
            }

            return ServiceResponse<Account>.Failure();
        }

        [ItemNotNull]
        public async Task<ServiceResponse<List<Account>>> FindAccountsStartingWithName(string name)
        {
            var accounts = (await _accountRepository.FindAccounts(acc => acc.UserName.StartsWith(name)))
	            .ConvertAll(accDbModel => accDbModel?.ToModel());

			foreach (var account in accounts.Where(account => account != null))
			{
				account.ProfilePicturePath = (await _profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;
			}

            return ServiceResponse<List<Account>>.Success(accounts);
        }

        [ItemCanBeNull]
        public async Task<ServiceResponse<Account>> FindByEmail(string email)
        {
            var account = (await _accountRepository.FindAccount(acc => email.Equals(acc.Email)))?.ToModel();

            if (account == null)
            {
	            return ServiceResponse<Account>.Failure();
            }

            account.ProfilePicturePath = (await _profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;

            return ServiceResponse<Account>.Success(account);

        }

        public async Task<ServiceResponse> AddAsFriend(AddAsFriendMessage message)
        {
	        var addResult = await _friendsService.AddFriend(message.SessionContext, message.AccountId);

            return addResult ? ServiceResponse.Success() : ServiceResponse.Failure();
        }

        public async Task<ServiceResponse<List<Friend>>> GetFriends(ISessionContext sessionContext)
        {
            var friends = await _friendsService.GetFriends(sessionContext);

            await friends.ForEachAsync(async friend =>
            {
	            var profilePictureResponse = await _profilePictureService.GetProfilePictureForAccountById(friend.Id);

	            if (profilePictureResponse.IsSuccess)
	            {
		            friend.ProfilePicturePath = profilePictureResponse.PayloadOrFail;
	            }
            });

            return ServiceResponse<List<Friend>>.Success(friends);
        }
    }
}
