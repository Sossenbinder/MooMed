using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Database;

namespace MooMed.Module.Accounts.Repository
{
    public class AccountValidationDataHelper
    {
	    [NotNull]
	    private readonly AccountDbContextFactory m_accountDbContextFactory;

	    public AccountValidationDataHelper([NotNull] AccountDbContextFactory accountDbContextFactory)
	    {
		    m_accountDbContextFactory = accountDbContextFactory;
	    }

        /// <summary>
        /// Adds a new validation key for a given account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [ItemNotNull]
        public async Task<AccountValidationDbModel> CreateEmailValidationKey(int accountId)
        {
            var accountEmailValidationDbModel = new AccountValidationDbModel()
            {
                AccountId = accountId,
                ValidationGuid = Guid.NewGuid()
            };

            using(var ctx = m_accountDbContextFactory.CreateContext())
            {
                ctx.AccountEmailValidation.Add(accountEmailValidationDbModel);
                await ctx.SaveChangesAsync();
            }

            return accountEmailValidationDbModel;
        }

        /// <summary>
        /// Check the given validation token for an account and update it's validation status if it should be okaY
        /// </summary>
        /// <param name="accountValidationTokenData">Object containing accountId and validation token</param>
        /// <returns>Result whether the accountValidation was successful</returns>
        public async Task<AccountValidationResult> CheckAndUpdateValidation([NotNull] AccountValidationTokenData accountValidationTokenData)
        {
            using(var ctx = m_accountDbContextFactory.CreateContext())
            {
                var candidate = await ctx.AccountEmailValidation
	                .Where(val => val.AccountId == accountValidationTokenData.AccountId)
	                .Include(x => x.AccountEntity)
	                .FirstOrDefaultAsync();

                // If we don't find an account here, it is most likely already validated and the entry is gone for good
                if (candidate == null)
                {
                    return AccountValidationResult.AlreadyValidated;
                }

                // If we find a candidate but it is already validated, we should remove it from here
                if (candidate.AccountEntity.EmailValidated)
                {
                    return AccountValidationResult.AlreadyValidated;
                }

                // If candidate has a token which is not equal to the given one, it can be considered invalid
                if (!candidate.ValidationGuid.Equals(accountValidationTokenData.ValidationGuid))
                {
                    return AccountValidationResult.TokenInvalid;
                }

                // Update validation status
                candidate.AccountEntity.EmailValidated = true;
                await ctx.SaveChangesAsync();

                return AccountValidationResult.Success;

            }
        }

        public async Task DeleteValidationDetails(int accountId)
        {
            using (var ctx = m_accountDbContextFactory.CreateContext())
            {
                var validation = await ctx.AccountEmailValidation.FirstAsync(x => x.AccountId == accountId);

                ctx.AccountEmailValidation.Remove(validation);

                await ctx.SaveChangesAsync();
            }
        }
    }
}
