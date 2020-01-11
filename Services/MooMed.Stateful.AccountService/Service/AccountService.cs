using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations;
using MooMed.Module.Accounts.Events.Interface;
using MooMed.Module.Accounts.Helper.Interface;
using MooMed.Module.Accounts.Repository;
using ProtoBuf.Meta;

namespace MooMed.Stateful.AccountService.Service
{
    public class AccountService : Common.ServiceBase.MooMedServiceBase, IAccountService
    {
	    [NotNull]
        private readonly IAccountEventHub m_accountEventHub;

        [NotNull]
        private readonly IAccountSignInService m_accountSignInService;

        [NotNull]
        private readonly AccountDataRepository m_accountDataRepository;

        [NotNull]
        private readonly IProfilePictureService m_profilePictureService;

        [NotNull]
        private readonly ISessionService m_sessionService;

        public AccountService(
            [NotNull] IMainLogger logger,
            [NotNull] IAccountSignInService accountSignInService,
            [NotNull] IAccountEventHub accountEventHub,
            [NotNull] AccountDataRepository accountDataRepository,
            [NotNull] IProfilePictureService profilePictureService,
            [NotNull] ISessionService sessionService)
            : base(logger)
        {
            m_accountSignInService = accountSignInService;
            m_accountEventHub = accountEventHub;
            m_accountDataRepository = accountDataRepository;
            m_profilePictureService = profilePictureService;
            m_sessionService = sessionService;
        }
        /// <summary>
        /// Login an account
        /// </summary>
        /// <param name="loginModel">Login model of that account</param>
        /// <returns></returns>
        [ItemNotNull]
        public async Task<ServiceResponse<LoginResult>> Login(LoginModel loginModel)
        {
	        var test = RuntimeTypeModel.Default.GetTypes();
            // Do the actual login in the AccountManager
            var loginResult = await m_accountSignInService.Login(loginModel);
            
            if (!loginResult.IsSuccess)
            {
                return loginResult;
            }
			
            var payload = loginResult.PayloadOrFail;

            payload.Account.ProfilePicturePath = await m_profilePictureService.GetProfilePictureForAccountById(payload.Account.Id);

            var sessionContext = await m_sessionService.LoginAccount(payload.Account);

            Logger.Info("Login happened", sessionContext);

            await m_accountEventHub.AccountLoggedIn.Raise(sessionContext);

            return loginResult;
        }
        private void PopulateTypes(Type t)
        {
	        foreach (object mt in RuntimeTypeModel.Default.GetTypes())
	        {
		        MetaType theType = mt as MetaType;
		        if (null != theType)
		        {
			        if (theType.Type == t)
				        return;
		        }
	        }

	        Type objType = typeof(object);
	        List<Type> inheritanceTree = new List<Type>();
	        do
	        {
		        inheritanceTree.Insert(0, t);
		        t = t.BaseType;
	        } while (null != t && t != objType);

	        if (!inheritanceTree.Any(gt => gt.IsGenericType))
		        return;

	        int n = 100;
	        for (int i = 0; i < inheritanceTree.Count - 1; i++)
	        {
		        Type type = inheritanceTree[i];
		        MetaType mt = RuntimeTypeModel.Default.Add(type, true);
		        mt.AddSubType(n++, inheritanceTree[i + 1]);
	        }
        }

        /// <summary>
        /// Refresh login for an account which is already authenticated but lost its session
        /// </summary>
        /// <param name="accountIdQuery">Account id of account to relogin</param>
        /// <returns></returns>
        public async Task RefreshLoginForAccount(AccountIdQuery accountIdQuery)
        {
            var account = await FindById(accountIdQuery);

            if (account == null)
            {
                throw new AuthenticationException("Account logged in but could not be found");
			}

			account.ProfilePicturePath = await m_profilePictureService.GetProfilePictureForAccountById(accountIdQuery.AccountId);


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
        public async Task<RegistrationResult> Register(RegisterModel registerModel, Language lang)
        {
            var registrationResult = await m_accountSignInService.Register(registerModel);

            if (registrationResult.IsSuccess)
            {
                await m_accountEventHub.AccountRegistered.Raise((registrationResult.Account, lang));
            }

            return registrationResult;
        }

        public async Task LogOff(ISessionContext sessionContext)
        {
            await m_accountEventHub.AccountLoggedOut.Raise(sessionContext);
        }

        [ItemCanBeNull]
        public async Task<Account> FindById(AccountIdQuery accountIdQuery)
        {
            var account = (await m_accountDataRepository.FindAccount(acc => acc.Id == accountIdQuery.AccountId))?.ToModel();

            if (account != null)
            {
                account.ProfilePicturePath = await m_profilePictureService.GetProfilePictureForAccountById(account.Id);
            }

            return account;
        }

        [ItemNotNull]
        public async Task<List<Account>> FindAccountsStartingWithName(string name)
        {
            var accounts = (await m_accountDataRepository.FindAccounts(acc => acc.UserName.StartsWith(name)))
	            .ConvertAll(accDbModel => accDbModel?.ToModel());

			foreach (var account in accounts)
            {
                if (account != null)
                {
                    account.ProfilePicturePath = await m_profilePictureService.GetProfilePictureForAccountById(account.Id);
                }
            }

            return accounts;
        }

        [ItemCanBeNull]
        public async Task<Account> FindByEmail(string email)
        {
            var account = (await m_accountDataRepository.FindAccount(acc => email.Equals(acc.Email)))?.ToModel();

            if (account != null)
            {
                account.ProfilePicturePath = await m_profilePictureService.GetProfilePictureForAccountById(account.Id);
            }

            return account;
        }
    }
}
