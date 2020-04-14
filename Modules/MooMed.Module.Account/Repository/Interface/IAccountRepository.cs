using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Repository.Interface
{
	public interface IAccountRepository : ICrudRepository<AccountEntity, int>
	{
		[ItemNotNull]
		Task<AccountEntity> CreateAccount([NotNull] RegisterModel registerModel);

		Task UpdateLastAccessedAt([NotNull] AccountEntity accountEntity);

		[ItemCanBeNull]
		Task<AccountEntity> FindAccount([NotNull] Expression<Func<AccountEntity, bool>> predicateFunc);

		[ItemNotNull]
		Task<List<AccountEntity>> FindAccounts([NotNull] Expression<Func<AccountEntity, bool>> predicateFunc);

		[NotNull]
		Task<bool> RefreshLastAccessedAt([NotNull] ISessionContext sessionContext);
	}
}
