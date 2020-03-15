using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Repository;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Interface;

namespace MooMed.Module.Accounts.Repository
{
    public class AccountValidationDataHelper : AbstractCrudRepository<AccountDbContextFactory, AccountDbContext, AccountValidationEntity>, IAccountValidationDataHelper
    {
	    [NotNull]
	    private readonly AccountDbContextFactory m_accountDbContextFactory;

	    public AccountValidationDataHelper([NotNull] AccountDbContextFactory accountDbContextFactory) 
		    : base(accountDbContextFactory)
	    {
		    m_accountDbContextFactory = accountDbContextFactory;
	    }

        /// <summary>
        /// Check the given validation token for an account and update it's validation status if it should be okaY
        /// </summary>
        /// <param name="accountValidationTokenData">Object containing accountId and validation token</param>
        /// <returns>Result whether the accountValidation was successful</returns>
        public async Task<AccountValidationResult> CheckAndUpdateValidation([NotNull] AccountValidationTokenData accountValidationTokenData)
        {
	        await using(var ctx = m_accountDbContextFactory.CreateContext())
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
