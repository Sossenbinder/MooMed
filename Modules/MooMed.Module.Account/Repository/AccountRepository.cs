using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Converter;
using MooMed.Common.Database.Repository;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.Code.Helper.Crypto;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Interface;

namespace MooMed.Module.Accounts.Repository
{
    public class AccountRepository : AbstractCrudRepository<AccountDbContextFactory, AccountDbContext, AccountEntity, int>, IAccountRepository
    {
	    [NotNull]
	    private readonly IEntityConverter<RegisterModel, AccountEntity, int> m_registerModelToAccountConverter;

        public AccountRepository(
		    [NotNull] AccountDbContextFactory contextFactory,
		    [NotNull] IEntityConverter<RegisterModel, AccountEntity, int> registerModelToAccountConverter) 
		    : base(contextFactory)
	    {
		    m_registerModelToAccountConverter = registerModelToAccountConverter;
        }

        public async Task<AccountEntity> CreateAccount(RegisterModel registerModel)
        {
	        var accountDbModel = m_registerModelToAccountConverter.ToEntity(registerModel);
	        accountDbModel.PasswordHash = Sha256Helper.Hash(registerModel.Password);

            await Create(accountDbModel);

            return accountDbModel;
        }

        public async Task UpdateLastAccessedAt(AccountEntity accountEntity)
        {
            await using var ctx = CreateContext();

            // Don't try-catch here, if there's an error resolving this accountId, that is catastrophic failure
            var account = await ctx.Account.Where(acc => acc.Id == accountEntity.Id).FirstAsync();

            account.LastAccessedAt = DateTime.Now;

            await ctx.SaveChangesAsync();
        }

        public async Task<AccountEntity> FindAccount(Expression<Func<AccountEntity, bool>> predicateFunc)
        {
	        await using var ctx = CreateContext();
            
            var suitableAccount = ctx.Account.Where(predicateFunc);

            if (await suitableAccount.AnyAsync())
            {
                return await suitableAccount.FirstAsync();
            }

            return null;
        }

        public async Task<List<AccountEntity>> FindAccounts(Expression<Func<AccountEntity, bool>> predicateFunc)
        {
	        await using var ctx = CreateContext();

            var accList = ctx.Account.Where(predicateFunc);

            if (accList.Any())
            {
                return await accList.ToListAsync();
            }

            return new List<AccountEntity>();
        }

        public async Task<bool> RefreshLastAccessedAt(ISessionContext sessionContext)
        {
            int rowsAffected;
            using (var ctx = CreateContext())
            {
                var account = await ctx.Account.FirstAsync(acc => acc.Id.Equals(sessionContext.Account.Id));
                account.LastAccessedAt = DateTime.Now;

                rowsAffected = await ctx.SaveChangesAsync();
            }

            return rowsAffected == 1;
        }
	}
}
