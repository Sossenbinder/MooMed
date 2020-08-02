using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface IUserService
	{
		[ItemCanBeNull]
		Task<Account> FindById([NotNull] int accountId);

		[NotNull]
		Task<List<Account>> FindAccountsStartingWithName([NotNull] string name);

		[ItemCanBeNull]
		Task<Account> FindByEmail([NotNull] string email);
	}
}