using JetBrains.Annotations;
using MooMed.Common.Database.Repository;
using MooMed.Module.Accounts.Database;
using MooMed.Module.Accounts.Datatypes.Entity;
using MooMed.Module.Accounts.Repository.Interface;

namespace MooMed.Module.Accounts.Repository
{
	public class AccountOnlineStateRepository : AbstractCrudRepository<AccountDbContextFactory, AccountDbContext, AccountOnlineStateEntity, int>, IAccountOnlineStateRepository
	{
		public AccountOnlineStateRepository([NotNull] AccountDbContextFactory contextFactory) 
			: base(contextFactory)
		{
		}
	}
}
