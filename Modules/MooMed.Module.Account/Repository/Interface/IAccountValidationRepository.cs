using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Repository.Interface
{
	public interface IAccountValidationRepository : ICrudRepository<AccountValidationEntity, int>
	{
		Task<AccountValidationResult> CheckAndUpdateValidation([NotNull] AccountValidationTokenData accountValidationTokenData);
	}
}
