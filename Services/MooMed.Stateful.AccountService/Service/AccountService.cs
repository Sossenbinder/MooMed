using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Messages.Account;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Repository;
using MooMed.Module.Accounts.Repository.Interface;
using MooMed.Module.Accounts.Service.Interface;

namespace MooMed.Stateful.AccountService.Service
{
    public class AccountService : Common.ServiceBase.MooMedServiceBase, IAccountService
    {
	    [NotNull]
        private readonly IAccountEventHub m_accountEventHub;

        [NotNull]
        private readonly IAccountSignInService m_accountSignInService;

        [NotNull]
        private readonly IAccountDataRepository m_accountDataRepository;

        [NotNull]
        private readonly IProfilePictureService m_profilePictureService;

        [NotNull]
        private readonly ISessionService m_sessionService;

        [NotNull]
        private readonly IAccountValidationService m_accountValidationService;

        [NotNull]
        private readonly IFriendsService m_friendsService;

        public AccountService(
            [NotNull] IMainLogger logger,
            [NotNull] IAccountSignInService accountSignInService,
            [NotNull] IAccountEventHub accountEventHub,
            [NotNull] IAccountDataRepository accountDataRepository,
            [NotNull] IProfilePictureService profilePictureService,
            [NotNull] ISessionService sessionService,
            [NotNull] IAccountValidationService accountValidationService,
            [NotNull] IFriendsService friendsService)
            : base(logger)
        {
            m_accountSignInService = accountSignInService;
            m_accountEventHub = accountEventHub;
            m_accountDataRepository = accountDataRepository;
            m_profilePictureService = profilePictureService;
            m_sessionService = sessionService;
            m_accountValidationService = accountValidationService;
            m_friendsService = friendsService;
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
            var loginResult = await m_accountSignInService.Login(loginModel);
            
            if (!loginResult.IsSuccess)
            {
                return loginResult;
            }
			
            var payload = loginResult.PayloadOrFail;
            
            var profilePictureResponse = await m_profilePictureService.GetProfilePictureForAccountById(payload.Account.Id);

            payload.Account.ProfilePicturePath = profilePictureResponse.PayloadOrNull;

            var sessionContext = await m_sessionService.LoginAccount(payload.Account);

            Logger.Info("Login happened", sessionContext);

            await m_accountEventHub.AccountLoggedIn.Raise(sessionContext);

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

            var profilePictureResponse = await m_profilePictureService.GetProfilePictureForAccountById(accountId);

            account.ProfilePicturePath = profilePictureResponse.PayloadOrNull;

			var sessionContext = await m_sessionService.LoginAccount(account);

            await m_accountSignInService.RefreshLastAccessed(sessionContext);

            await m_accountEventHub.AccountLoggedIn.Raise(sessionContext);
        }

        /// <summary>
        /// Registers an account and logs it in if the register was successful
        /// </summary>
        /// <param name="registerModel">Register model of that account</param>
        /// <returns></returns>
        [ItemNotNull]
        public async Task<ServiceResponse<RegistrationResult>> Register(RegisterModel registerModel)
        {
	        var registrationResult = await m_accountSignInService.Register(registerModel);

            if (registrationResult.IsSuccess)
            {
                _ = Task.Run(() =>
                      m_accountValidationService.SendAccountValidationMail(new AccountValidationMailData()
                      {
                          Account = registrationResult.Account,
                          Language = registerModel.Language
                      }));
            }

            return ServiceResponse<RegistrationResult>.Success(registrationResult);
        }

        public async Task<ServiceResponse> LogOff(ISessionContext sessionContext)
        {
            await m_accountEventHub.AccountLoggedOut.Raise(sessionContext);

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse<Account>> FindById(Primitive<int> accountId)
        {
            var account = (await m_accountDataRepository.FindAccount(acc => acc.Id == accountId))?.ToModel();

            if (account != null)
            {
                account.ProfilePicturePath = (await m_profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;

                return ServiceResponse<Account>.Success(account);
            }

            return ServiceResponse<Account>.Failure();
        }

        [ItemNotNull]
        public async Task<ServiceResponse<List<Account>>> FindAccountsStartingWithName(string name)
        {
            var accounts = (await m_accountDataRepository.FindAccounts(acc => acc.UserName.StartsWith(name)))
	            .ConvertAll(accDbModel => accDbModel?.ToModel());

			foreach (var account in accounts.Where(account => account != null))
			{
				account.ProfilePicturePath = (await m_profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;
			}

            return ServiceResponse<List<Account>>.Success(accounts);
        }

        [ItemCanBeNull]
        public async Task<ServiceResponse<Account>> FindByEmail(string email)
        {
            var account = (await m_accountDataRepository.FindAccount(acc => email.Equals(acc.Email)))?.ToModel();

            if (account == null)
            {
	            return ServiceResponse<Account>.Failure();
            }

            account.ProfilePicturePath = (await m_profilePictureService.GetProfilePictureForAccountById(account.Id)).PayloadOrNull;

            return ServiceResponse<Account>.Success(account);

        }

        public async Task<ServiceResponse> AddAsFriend(AddAsFriendMessage message)
        {
	        var addResult = await m_friendsService.AddFriend(message.SessionContext, message.AccountId);

            return addResult ? ServiceResponse.Success() : ServiceResponse.Failure();
        }

        public async Task<ServiceResponse<List<Friend>>> GetFriends(ISessionContext sessionContext)
        {
            var friends = await m_friendsService.GetFriends(sessionContext);

            return ServiceResponse<List<Friend>>.Success(friends);
        }
    }
}
