using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.Code.Database.Repository;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Repository.Interface;

namespace MooMed.Module.Accounts.Repository
{
    public class AccountDataRepository : AbstractCrudRepository<AccountDbContext, AccountEntity>, IAccountDataRepository
	{
        [ItemNotNull]
        public async Task<AccountEntity> CreateAccount([NotNull] RegisterModel registerModel)
        {
	        var accountDbModel = registerModel.ToAccountEntity();

            await Create(accountDbModel);

            return accountDbModel;
        }

        public async Task UpdateLastAccessedAt([NotNull] AccountEntity accountEntity)
        {
            using (var ctx = AccountDbContext.Create())
            {
                // Don't try-catch here, if there's an error resolving this accountId, that is catastrophic failure
                var account = await ctx.Account.Where(acc => acc.Id == accountEntity.Id).FirstAsync();

                account.LastAccessedAt = DateTime.Now;

                await ctx.SaveChangesAsync();

            }
        }

        [ItemCanBeNull]
        public async Task<AccountEntity> FindAccount([NotNull] Expression<Func<AccountEntity, bool>> predicateFunc)
        {
            using (var ctx = AccountDbContext.Create())
            {
                var suitableAccount = ctx.Account.Where(predicateFunc);

                if (await suitableAccount.AnyAsync())
                {
                    return await suitableAccount.FirstAsync();
                }

                return null;
            }
        }

        [ItemNotNull]
        public async Task<List<AccountEntity>> FindAccounts([NotNull] Expression<Func<AccountEntity, bool>> predicateFunc)
        {
            using (var ctx = AccountDbContext.Create())
            {
                var accList = ctx.Account.Where(predicateFunc);

                if (accList.Any())
                {
                    return await accList.ToListAsync();
                }

                return new List<AccountEntity>();
            }
        }

        [NotNull]
        public async Task<bool> RefreshLastAccessedAt([NotNull] ISessionContext sessionContext)
        {
            int rowsAffected;
            using(var ctx = AccountDbContext.Create())
            {
                var account = await ctx.Account.FirstAsync(acc => acc.Id.Equals(sessionContext.Account.Id));
                account.LastAccessedAt = DateTime.Now;

                rowsAffected = await ctx.SaveChangesAsync();
            }

            return rowsAffected == 1;
        }
    }
}
